USE [ERP_Mercury]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetDiscountPercentByTypeFromXml] ( @doc xml (DOCUMENT DiscountList), @DiscountTypeValue int)
returns decimal(10, 4)
with execute as caller
as
begin
 DECLARE @ReturnValue decimal(10, 4);
 SET @ReturnValue = 0;


 IF( @DiscountTypeValue = 0 )
  SELECT @ReturnValue = @doc.value( '(//DiscountList/DiscountItem[@DiscountType=0]/@DiscountPercent)[1]', 'decimal(10, 4)' ) ;

 IF( @DiscountTypeValue = 1 )
  SELECT @ReturnValue = @doc.value( '(//DiscountList/DiscountItem[@DiscountType=1]/@DiscountPercent)[1]', 'decimal(10, 4)' ) ;

 IF( @DiscountTypeValue = 2 )
  SELECT @ReturnValue = @doc.value( '(//DiscountList/DiscountItem[@DiscountType=2]/@DiscountPercent)[1]', 'decimal(10, 4)' ) ;

 IF( @DiscountTypeValue = 3 )
  SELECT @ReturnValue = @doc.value( '(//DiscountList/DiscountItem[@DiscountType=3]/@DiscountPercent)[1]', 'decimal(10, 4)' ) ;

 IF( @DiscountTypeValue = 4 )
  SELECT @ReturnValue = @doc.value( '(//DiscountList/DiscountItem[@DiscountType=4]/@DiscountPercent)[1]', 'decimal(10, 4)' ) ;

 IF( @DiscountTypeValue = 5 )
  SELECT @ReturnValue = @doc.value( '(//DiscountList/DiscountItem[@DiscountType=5]/@DiscountPercent)[1]', 'decimal(10, 4)' ) ;

 IF( @DiscountTypeValue = 6 )
  SELECT @ReturnValue = @doc.value( '(//DiscountList/DiscountItem[@DiscountType=6]/@DiscountPercent)[1]', 'decimal(10, 4)' ) ;

 IF( @ReturnValue IS NULL ) SET @ReturnValue = 0;
 
 RETURN @ReturnValue;

end

GO
GRANT EXECUTE ON [dbo].[GetDiscountPercentByTypeFromXml] TO [public]
GO