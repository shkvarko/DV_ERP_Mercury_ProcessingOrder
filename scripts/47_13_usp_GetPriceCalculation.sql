USE [ERP_Mercury]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetPriceCalculation]    Script Date: 04/01/2012 18:22:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[usp_GetPriceCalculation] 
	@Parts_Guid D_GUID,
	@PaymentType_Guid D_GUID,
	@Stock_Guid D_GUID,
	@DiscountPercent decimal(18, 4) = 0,

	@NDSPercent D_MONEY output,
  @PriceImporter D_MONEY output,
  @Price D_MONEY output,
  @PriceWithDiscount D_MONEY output,
  @PriceInAccountingCurrency D_MONEY output,
  @PriceWithDiscountInAccountingCurrency D_MONEY output,
  @IsPartsImporter D_YESNO output,-- признак "товар импортера" 
  @ChargesPercent D_MONEY output,  -- процент надбавки по товару

  @PriceImporterFromPriceList D_MONEY output,
  @PriceFromPriceList D_MONEY output,
  @PriceInAccountingCurrencyFromPriceList D_MONEY output,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN
SET NOCOUNT ON;

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';
  SET @PriceImporter = 0;
  SET @Price = 0;
  SET @PriceWithDiscount = 0;
  SET @PriceInAccountingCurrency = 0;
  SET @PriceWithDiscountInAccountingCurrency = 0;
  SET @NDSPercent = 0;
  SET @IsPartsImporter = 0;
  SET @ChargesPercent = 0;

	BEGIN TRY


    -- Признак "товар импортера"
    SELECT @IsPartsImporter = dbo.GetPropertieImporterForStockGuid( @Stock_Guid, @Parts_Guid );
    IF ( @IsPartsImporter IS NULL )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  ' [usp_GetPrice] Не удалось определить признак "товар импортера".';

	      RETURN @ERROR_NUM;
      END 

    -- Ставка НДС, %
    SELECT @NDSPercent = dbo.GetNDSPercentForPartsGuid(@Parts_Guid);
    IF ( @NDSPercent IS NULL )
      BEGIN
        SET @ERROR_NUM = 2;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  ' [usp_GetPrice] Не удалось определить ставку НДС.';

	      RETURN @ERROR_NUM;
      END 

    -- Размер надбавки по товару, %
    SELECT @ChargesPercent = dbo.GetPropertieChargeForStockAndParts( @Stock_Guid, @Parts_Guid );
    IF ( @ChargesPercent IS NULL )
      BEGIN
        SET @ERROR_NUM = 3;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  ' [usp_GetPrice] Не удалось определить размер надбавки.';

	      RETURN @ERROR_NUM;
      END 
      
    -- форма оплаты
    DECLARE @PaymentTypeForm1Guid D_GUID;
    SELECT @PaymentTypeForm1Guid = [dbo].[GetPaymentTypeForm1Guid]();
    
    DECLARE @PaymentTypeForm2Guid D_GUID;
    SELECT @PaymentTypeForm2Guid = [dbo].[GetPaymentTypeForm2Guid]();
    
     
    -- Не смотря на все препоны и проверки, дело дошло до прайс-листа
    -- Цены из прайс-листа
    DECLARE @Price0 decimal(18, 4);      -- Цена импортера, руб.
    DECLARE @Price0_2 decimal(18, 4);    -- Цена отпускная по прейскуранту с НДС, у.е.
    DECLARE @Price2 decimal(18, 4);      -- Цена отпускная по прейскуранту с НДС, руб.  
    DECLARE @Price11 decimal(18, 4);     -- Цена Регионы спец, руб.    
    DECLARE @Price0_11 decimal(18, 4);   -- Цена «Регионы спец, у.е.» 
    DECLARE @CurrencyRatePricing float; 
    
    SELECT @Price0 = CONVERT( decimal(18, 4), Price0 ), 
      @Price0_2 = CONVERT( decimal(18, 4), Price0_2 ), 
      @Price2 = CONVERT( decimal(18, 4), Price2 ),
      @Price11 = CONVERT( decimal(18, 4), Price11 ),
      @Price0_11 = CONVERT( decimal(18, 4), Price0_11 )       
    FROM dbo.T_Prices WHERE Parts_Guid = @Parts_Guid;
    
--    SET @CurrencyRatePricing = 9000; -- заменить на запрос из функции

    SELECT @CurrencyRatePricing = dbo.GetCurrencyRatePricingInOut( dbo.GetCurrencyGuidUSD(), dbo.GetCurrencyGuidBYB(), dbo.TrimTime( GETDATE() ) );    
    IF( ( @CurrencyRatePricing IS NULL ) OR ( @CurrencyRatePricing = 0 ) )
      BEGIN
        SET @ERROR_NUM = 4;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  ' [usp_GetPrice] Не удалось определить курс ценообразования.';

	      RETURN @ERROR_NUM;
      END 
    
    IF( @Price0 IS NULL ) SET @Price0 = 0;
    IF( @Price0_2 IS NULL ) SET @Price0_2 = 0;
    IF( @Price2 IS NULL ) SET @Price2 = 0;
    IF( @Price11 IS NULL ) SET @Price11 = 0;
    IF( @Price0_11 IS NULL ) SET @Price0_11 = 0;
    
		IF( @PaymentType_Guid = @PaymentTypeForm1Guid )
			BEGIN
				-- форма оплаты №1

				SET @PriceImporterFromPriceList = @Price0;
				SET @PriceFromPriceList = @Price2;
				SET @PriceInAccountingCurrencyFromPriceList = @Price0_2;

				IF( ( @Price0 = 0 ) OR ( @Price2 = 0 ) )
					BEGIN
				    
						SET @ERROR_NUM = 10;
						SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + 
							'@Price0: ' + CONVERT( nvarchar(15), @Price0 ) + nChar(13) + nChar(10) + 
							'@Price0_2: ' + CONVERT( nvarchar(15), @Price0_2 )  + nChar(13) + nChar(10) + 
							'@Price2: ' + CONVERT( nvarchar(15), @Price2 );
						RETURN @ERROR_NUM;
					END 

				IF( @IsPartsImporter = 0 )
					BEGIN
						-- товар отечественный
						SET @PriceImporter = @Price0;
						SET @Price = ( SELECT dbo.GetRoundMoney( @Price0 * ( 1 + ( @ChargesPercent/100 ) ) ) );
						SET @Price = @Price * ( 1 + ( @NDSPercent/100 ) );
						SET @PriceWithDiscount = ( SELECT dbo.GetRoundMoney( @Price0 * ( 1 + ( @ChargesPercent - @DiscountPercent )/100 ) * ( 1 + ( @NDSPercent/100 ) ) ) );
						SET @PriceInAccountingCurrency = @Price/@CurrencyRatePricing;
						SET @PriceWithDiscountInAccountingCurrency = @PriceWithDiscount/@CurrencyRatePricing;
					END
				ELSE IF( @IsPartsImporter = 1 )	
					BEGIN
						-- товар импортный
						SET @PriceImporter = ( SELECT dbo.GetRoundMoney( @Price0 * ( 1 - ( @DiscountPercent/100 ) ) ) );
						SET @PriceWithDiscount = @PriceImporter * ( 1 + ( @NDSPercent/100 ) );
						SET @Price = @PriceWithDiscount;
						SET @PriceInAccountingCurrency = @Price/@CurrencyRatePricing;
						SET @PriceWithDiscountInAccountingCurrency = @PriceWithDiscount/@CurrencyRatePricing;
						
					END
			END
		ELSE IF( @PaymentType_Guid = @PaymentTypeForm2Guid )
			BEGIN
				-- форма оплаты №2
				SET @PriceImporterFromPriceList = @Price0;
				SET @PriceFromPriceList = @Price11;
				SET @PriceInAccountingCurrencyFromPriceList = @Price0_11;

				IF( ( @Price0 = 0 ) OR ( @Price0_11 = 0 )  OR ( @Price11 = 0 ))
					BEGIN
						SET @ERROR_NUM = 10;
						SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + 
							'@Price0: ' + CONVERT( nvarchar(15), @Price0 ) + nChar(13) + nChar(10) + 
							'@Price0_11: ' + CONVERT( nvarchar(15), @Price0_11 )  + nChar(13) + nChar(10) + 
							'@Price11: ' + CONVERT( nvarchar(15), @Price11 );
						RETURN @ERROR_NUM;
					END 
					
					SET @Price = @Price11;
					SET @PriceInAccountingCurrency = @Price0_11;
					SET @PriceImporter =  ( SELECT dbo.GetRoundMoney( ( @Price/( 1 + ( @NDSPercent/100 ) ) ) ) );

					IF( @IsPartsImporter = 1 )
						BEGIN
							SET @Price = @PriceImporter * ( 1 + ( @NDSPercent/100 ) );
						END

					SET @PriceWithDiscount = @Price * ( 1 - ( @DiscountPercent/100 ) );
					SET @PriceWithDiscountInAccountingCurrency = @PriceInAccountingCurrency * ( 1 - ( @DiscountPercent/100 ) );
				
			END


	END TRY
	BEGIN CATCH
		SET @ERROR_NUM = ERROR_NUMBER();
		SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  ' [usp_GetPrice] Текст ошибки: ' + ERROR_MESSAGE();

    RETURN @ERROR_NUM;
	END CATCH;

	IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = @ERROR_MES + ' [usp_GetPrice] Успешное завершение операции.';
		
	RETURN @ERROR_NUM;
END

