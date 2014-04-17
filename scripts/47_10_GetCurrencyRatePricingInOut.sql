USE [ERP_Mercury]
GO

/****** Object:  UserDefinedFunction [dbo].[GetCurrencyRateInOut]    Script Date: 04/01/2012 11:58:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Возвращает курс валюты @CurrencyIn к валюте @CurrencyOut на указанную дату

CREATE FUNCTION [dbo].[GetCurrencyRatePricingInOut] ( @CurrencyIn D_GUID, @CurrencyOut D_GUID,  @BEGINDATE D_DATE )
returns money
with execute as caller
as
begin
  DECLARE @ReturnValue money;
  SET @ReturnValue = NULL;
  
	DECLARE @CURRENCYRATE money;
	SET @CURRENCYRATE = 0;
  
  IF( @CurrencyIn = @CurrencyOut )
		BEGIN
			SET @CURRENCYRATE = 1;
		END
  ELSE
		BEGIN
			IF( ( @CurrencyIn IS NOT NULL ) AND ( @CurrencyOut IS NOT NULL ) )
				BEGIN
					-- Находим дату курса, ближайшую к @END_DATE
					DECLARE @CURRENCYRATE_DATE D_DATE;
					SELECT @CURRENCYRATE_DATE = MAX( CurrencyRate_Date ) FROM dbo.T_CurrencyRate_Pricing
					WHERE 
								( Currency_In_Guid = @CurrencyIn ) 
						AND ( Currency_Out_Guid = @CurrencyOut )
						AND ( CurrencyRate_Date <= @BEGINDATE );

					-- Если дата найдена, запрашиваем курс
					IF( @CURRENCYRATE_DATE IS NOT NULL )
						BEGIN
							SELECT @CURRENCYRATE = CurrencyRate_Value FROM dbo.T_CurrencyRate_Pricing
							WHERE 
										( Currency_In_Guid = @CurrencyIn ) 
								AND ( Currency_Out_Guid = @CurrencyOut )
								AND ( CurrencyRate_Date = @CURRENCYRATE_DATE );
						END
					ELSE
						BEGIN --
							-- Попробуем найти обратный курс
							-- Находим дату курса, ближайшую к @END_DATE
							SELECT @CURRENCYRATE_DATE = MAX( CurrencyRate_Date ) FROM dbo.T_CurrencyRate_Pricing
							WHERE 
										( Currency_In_Guid = @CurrencyOut ) 
								AND ( Currency_Out_Guid = @CurrencyIn )
								AND ( CurrencyRate_Date <= @BEGINDATE );

							-- Если дата найдена, запрашиваем курс
							IF( @CURRENCYRATE_DATE IS NOT NULL )
								BEGIN
									SELECT @CURRENCYRATE = CurrencyRate_Value FROM dbo.T_CurrencyRate_Pricing
									WHERE 
											( Currency_In_Guid = @CurrencyOut ) 
										AND ( Currency_Out_Guid = @CurrencyIn )
										AND ( CurrencyRate_Date = @CURRENCYRATE_DATE );

									IF( ( @CURRENCYRATE IS NOT NULL ) AND ( @CURRENCYRATE <> 0 ) )
										BEGIN
											SET @CURRENCYRATE = cast( ( 1/@CURRENCYRATE ) as money );
										END
								END
						END --
				END
		END

	SET @ReturnValue = @CURRENCYRATE;
  RETURN @ReturnValue;

end


GO
GRANT EXECUTE ON [dbo].[GetCurrencyRatePricingInOut] TO [public]
GO
