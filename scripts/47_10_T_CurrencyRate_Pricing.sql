USE [ERP_Mercury]
GO

/****** Object:  Table [dbo].[T_CurrencyRate_Pricing]    Script Date: 03/30/2012 15:38:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_CurrencyRate_Pricing](
	[CurrencyRate_Guid] [dbo].[D_GUID] NOT NULL,
	[CurrencyRate_Date] [dbo].[D_DATE] NOT NULL,
	[Currency_In_Guid] [dbo].[D_GUID] NOT NULL,
	[Currency_Out_Guid] [dbo].[D_GUID] NOT NULL,
	[Record_Updated] [dbo].[D_DATETIME] NULL,
	[Record_UserUdpated] [dbo].[D_NAMESHORT] NULL,
	[CurrencyRate_Value] [dbo].[D_CURRENCYRATE] NULL,
 CONSTRAINT [PK_T_CurrencyRate_Pricing] PRIMARY KEY CLUSTERED 
(
	[CurrencyRate_Guid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[T_CurrencyRate_Pricing]  WITH CHECK ADD  CONSTRAINT [FK_T_CurrencyRate_Pricing_T_Currency] FOREIGN KEY([Currency_In_Guid])
REFERENCES [dbo].[T_Currency] ([Currency_Guid])
GO

ALTER TABLE [dbo].[T_CurrencyRate_Pricing] CHECK CONSTRAINT [FK_T_CurrencyRate_Pricing_T_Currency]
GO

ALTER TABLE [dbo].[T_CurrencyRate_Pricing]  WITH CHECK ADD  CONSTRAINT [FK_T_CurrencyRate_Pricing_T_Currency1] FOREIGN KEY([Currency_Out_Guid])
REFERENCES [dbo].[T_Currency] ([Currency_Guid])
GO

ALTER TABLE [dbo].[T_CurrencyRate_Pricing] CHECK CONSTRAINT [FK_T_CurrencyRate_Pricing_T_Currency1]
GO

CREATE NONCLUSTERED INDEX [INDX_T_CurrencyRate_Pricing_Date] ON [dbo].[T_CurrencyRate_Pricing] 
(
	[CurrencyRate_Date] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [INDEX]
GO

CREATE NONCLUSTERED INDEX [INDX_T_CurrencyRate_Date] ON [dbo].[T_CurrencyRate] 
(
	[CurrencyRate_Date] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [INDEX]
GO

CREATE NONCLUSTERED INDEX [INDX_T_CurrencyRate_Archive_CurrencyRate_Guid] ON [dbo].[T_CurrencyRate_Archive] 
(
	[CurrencyRate_Guid] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [INDEX]
GO

CREATE NONCLUSTERED INDEX [INDX_T_CurrencyRate_Archive_CurrencyRate_Date] ON [dbo].[T_CurrencyRate_Archive] 
(
	[CurrencyRate_Date] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [INDEX]
GO

CREATE NONCLUSTERED INDEX [INDX_T_CurrencyRate_Archive_Currency_In_Guid] ON [dbo].[T_CurrencyRate_Archive] 
(
	[Currency_In_Guid] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [INDEX]
GO

CREATE NONCLUSTERED INDEX [INDX_T_CurrencyRate_Archive_Currency_Out_Guid] ON [dbo].[T_CurrencyRate_Archive] 
(
	[Currency_Out_Guid] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [INDEX]
GO

/****** Object:  StoredProcedure [dbo].[usp_SynchCurrencyRate]    Script Date: 03/30/2012 15:46:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Синхронизирует информацию в курсах ценообразования
--
-- Входящие параметры:
--	@RATE_DATE - дата
--	@IBLINKEDSERVERNAME - имя linked-server InterBase
--
-- Выходные параметры:
--  @ERROR_NUM - № ошибки
--  @ERROR_MES - сообщение об ошибке
--
-- Результат:
--    0 - Успешное завершение
--    <> 0 - ошибка

CREATE PROCEDURE [dbo].[usp_SynchCurrencyRatePricing] 
	@RATE_DATE D_DATE = NULL,
  @IBLINKEDSERVERNAME dbo.D_NAME,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = NULL;
  IF( @RATE_DATE IS NULL )
    SET @RATE_DATE = GetDate();

  BEGIN TRY
    SELECT @RATE_DATE = dbo.TrimTime( @RATE_DATE );
	  DECLARE @strBEGIN_DATE varchar(10);
    SET @strBEGIN_DATE = CONVERT (varchar(10), @RATE_DATE, 104 );

    DECLARE @uuidBYB D_GUID;
    DECLARE @strBYB D_CURRENCYCODE;
    DECLARE @strBYB2 D_CURRENCYCODE;
    SET @strBYB = 'BYB';
    SELECT @uuidBYB = Currency_Guid FROM dbo.T_Currency WHERE Currency_Abbr = @strBYB;
    IF( @uuidBYB IS NULL )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = 'Не удалось найти идентификатор валюты с кодом "BYB".';
        RETURN @ERROR_NUM;
      END

    DECLARE @uuidCurrency D_GUID;
    DECLARE @strCurrency D_CURRENCYCODE;
    DECLARE @SQLString nvarchar(500);
    DECLARE @ParmDefinition nvarchar(500);
    SET @ParmDefinition = N'@RETURN_CODE float OUTPUT'; 
    DECLARE @RETURNVALUE float;

    DECLARE crCurrency CURSOR 
      FOR SELECT Currency_Guid, Currency_Abbr FROM dbo.T_Currency  WHERE ( Currency_Guid <> @uuidBYB )
    OPEN crCurrency;
    FETCH next FROM crCurrency INTO @uuidCurrency, @strCurrency;
    WHILE @@fetch_status = 0
	    BEGIN

        SET @RETURNVALUE = 0;
        IF( @strCurrency = 'RUB' ) SET @strCurrency = 'RUR';
        SET @SQLString = 'SELECT @RETURN_CODE = RETURN_VALUE FROM OPENQUERY( ' + @IBLINKEDSERVERNAME + 
          ', ''SELECT RETURN_VALUE FROM SP_GETCURRENCYRATE_PRICING( '  +  
          '''''' +	CAST( @strBEGIN_DATE as varchar(20)) +  ''''', ' +
          '''''' +	CAST( @strCurrency as varchar(3)) +  ''''', ' + 
          '''''' +	CAST( @strBYB as varchar(3)) +  ''''' ) '' )';

        EXECUTE sp_executesql @SQLString, @ParmDefinition, @RETURN_CODE = @RETURNVALUE OUTPUT

        IF( @RETURNVALUE <> 0 )
          BEGIN
            IF NOT EXISTS ( SELECT CurrencyRate_Guid FROM  dbo.T_CurrencyRate_Pricing
              WHERE ( Currency_In_Guid = @uuidCurrency ) 
                AND ( Currency_Out_Guid = @uuidBYB ) 
                AND ( CurrencyRate_Date = @RATE_DATE ) )
              BEGIN
                INSERT INTO dbo.T_CurrencyRate_Pricing( CurrencyRate_Guid, CurrencyRate_Date, Currency_In_Guid, Currency_Out_Guid, CurrencyRate_Value )
                VALUES( NewID(), @RATE_DATE, @uuidCurrency, @uuidBYB, @RETURNVALUE );
              END
            ELSE
              BEGIN
                UPDATE dbo.T_CurrencyRate_Pricing SET CurrencyRate_Value = @RETURNVALUE
                WHERE ( Currency_In_Guid = @uuidCurrency ) 
                  AND ( Currency_Out_Guid = @uuidBYB ) 
                  AND ( CurrencyRate_Date = @RATE_DATE ) 
              END
	        END

		    FETCH next FROM crCurrency INTO @uuidCurrency, @strCurrency;
	    END
    CLOSE crCurrency;
    DEALLOCATE crCurrency;

    SET @strBYB = 'USD';
    SELECT @uuidBYB = Currency_Guid FROM dbo.T_Currency WHERE Currency_Abbr = @strBYB;
    IF( @uuidBYB IS NULL )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = 'Не удалось найти идентификатор валюты с кодом "USD".';
        RETURN @ERROR_NUM;
      END
    DECLARE crCurrency CURSOR 
      FOR SELECT Currency_Guid, Currency_Abbr FROM dbo.T_Currency  WHERE ( Currency_Guid <> @uuidBYB )
    OPEN crCurrency;
    FETCH next FROM crCurrency INTO @uuidCurrency, @strCurrency;
    WHILE @@fetch_status = 0
	    BEGIN

        SET @RETURNVALUE = 0;
        IF( @strCurrency = 'RUB' ) SET @strCurrency = 'RUR';
        SET @SQLString = 'SELECT @RETURN_CODE = RETURN_VALUE FROM OPENQUERY( ' + @IBLINKEDSERVERNAME + 
          ', ''SELECT RETURN_VALUE FROM SP_GETCURRENCYRATE_PRICING( '  +  
          '''''' +	CAST( @strBEGIN_DATE as varchar(20)) +  ''''', ' +
          '''''' +	CAST( @strCurrency as varchar(3)) +  ''''', ' + 
          '''''' +	CAST( @strBYB as varchar(3)) +  ''''' ) '' )';

        EXECUTE sp_executesql @SQLString, @ParmDefinition, @RETURN_CODE = @RETURNVALUE OUTPUT

        IF( @RETURNVALUE <> 0 )
          BEGIN
            IF NOT EXISTS ( SELECT CurrencyRate_Guid FROM  dbo.T_CurrencyRate_Pricing
              WHERE ( Currency_In_Guid = @uuidCurrency ) 
                AND ( Currency_Out_Guid = @uuidBYB ) 
                AND ( CurrencyRate_Date = @RATE_DATE ) )
              BEGIN
                INSERT INTO dbo.T_CurrencyRate_Pricing( CurrencyRate_Guid, CurrencyRate_Date, Currency_In_Guid, Currency_Out_Guid, CurrencyRate_Value )
                VALUES( NewID(), @RATE_DATE, @uuidCurrency, @uuidBYB, @RETURNVALUE );
              END
            ELSE
              BEGIN
                UPDATE dbo.T_CurrencyRate_Pricing SET CurrencyRate_Value = @RETURNVALUE
                WHERE ( Currency_In_Guid = @uuidCurrency ) 
                  AND ( Currency_Out_Guid = @uuidBYB ) 
                  AND ( CurrencyRate_Date = @RATE_DATE ) 
              END
	        END

		    FETCH next FROM crCurrency INTO @uuidCurrency, @strCurrency;
	    END
    CLOSE crCurrency;
    DEALLOCATE crCurrency;

	END TRY
	BEGIN CATCH
		SET @ERROR_NUM = ERROR_NUMBER();
		SET @ERROR_MES = ERROR_MESSAGE();
		RETURN @ERROR_NUM;
	END CATCH;

	RETURN 0;
END


SET ANSI_NULLS ON

GO
GRANT EXECUTE ON [dbo].[usp_SynchCurrencyRatePricing] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetCurrencyGuidUSD]()
returns uniqueidentifier
with execute as caller
as
begin
 DECLARE @ReturnValue uniqueidentifier;
 SET @ReturnValue = 'B78BF75B-51CE-4FEA-95BD-399F16152C25';

 RETURN @ReturnValue;

end

GO
GRANT EXECUTE ON [dbo].[GetCurrencyGuidUSD] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetCurrencyGuidBYB]()
returns uniqueidentifier
with execute as caller
as
begin
 DECLARE @ReturnValue uniqueidentifier;
 SET @ReturnValue = '02E24CA4-1B35-4935-B7B7-415E5E340970';

 RETURN @ReturnValue;

end

GO
GRANT EXECUTE ON [dbo].[GetCurrencyGuidBYB] TO [public]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetPrice]    Script Date: 03/30/2012 16:13:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[usp_GetPrice] 
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

  DECLARE @IsPartsImporter D_YESNO  -- признак "товар импортера" 
  DECLARE @ChargesPercent D_MONEY;  -- процент надбавки по товару
  
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
    
    --SET @CurrencyRatePricing = 9000; -- заменить на запрос из функции
    
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

GO


