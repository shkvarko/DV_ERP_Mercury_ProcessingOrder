/*------ �������������� ��� � ������ ������  ------*/

SET TERM ~~~ ;
CREATE PROCEDURE SP_EDIT_SPLITMS_FROMSQL (
    ORDERITMS_SPLITMS_ID integer,
    PARTS_ID integer,
    MEASURE_ID integer,
    SPLITMS_PRICE double precision,
    SPLITMS_DISCOUNT double precision,
    SPLITMS_DISCOUNTPRICE double precision,
    SPLITMS_BASEPRICE double precision,
    SPLITMS_MARKUP double precision,
    SPLITMS_CURRENCYPRICE double precision,
    SPLITMS_CURRENCYDISCOUNTPRICE double precision,
    SPLITMS_DISCOUNTFROMPRICE double precision)
returns (
    RETURN_VALUE integer,
    SPLITMS_ID integer)
as
BEGIN
 RETURN_VALUE = -1;
 SPLITMS_ID = :ORDERITMS_SPLITMS_ID;

 IF( :RETURN_VALUE <> 0 ) then
  begin

 SELECT SPLITMS_ID FROM t_SPLITMS WHERE SPLITMS_ID = :ORDERITMS_SPLITMS_ID
 INTO :SPLITMS_ID;

 IF( ( :SPLITMS_ID IS NOT NULL ) AND ( :SPLITMS_BASEPRICE <> 0 ) ) THEN
  BEGIN
   UPDATE t_SPLITMS SET  SPLITMS_PRICE = :SPLITMS_PRICE,  SPLITMS_DISCOUNT = :SPLITMS_DISCOUNT,
    SPLITMS_DISCOUNTPRICE = :SPLITMS_DISCOUNTPRICE,  SPLITMS_BASEPRICE = :SPLITMS_BASEPRICE,
    SPLITMS_MARKUP = :SPLITMS_MARKUP, SPLITMS_CURRENCYPRICE = :SPLITMS_CURRENCYPRICE,
    SPLITMS_CURRENCYDISCOUNTPRICE = :SPLITMS_CURRENCYDISCOUNTPRICE,  SPLITMS_DISCOUNTFROMPRICE = :SPLITMS_DISCOUNTFROMPRICE
   WHERE SPLITMS_ID = :SPLITMS_ID;

   RETURN_VALUE = 0;
  END
  end


 SUSPEND;
END
 ~~~
SET TERM ; ~~~
commit work;

/*------ ����������� ������ � ������� � ������  ------*/

SET TERM ~~~ ;
create procedure SP_ADDSPLITMSDISCOUNTFROMSQL (
    SPLITMS_ID integer,
    DISCOUNTTYPE_ID integer,
    SPLITMS_DISCOUNTFROMPRICE_PER double precision,
    SPLITMS_DISCOUNTFROMPRICE_SUM double precision)
returns (
    RETURN_VALUE integer)
as
BEGIN
  RETURN_VALUE = -1;

  IF( exists ( SELECT SPLITMS_ID FROM T_SPLITMS WHERE SPLITMS_ID = :SPLITMS_ID ) ) THEN
    BEGIN
      DELETE FROM T_SPLITMS_DISCOUNT WHERE SPLITMS_ID = :SPLITMS_ID AND DISCOUNTTYPE_ID = :DISCOUNTTYPE_ID;

      INSERT INTO T_SPLITMS_DISCOUNT ( SPLITMS_ID,  DISCOUNTTYPE_ID,  SPLITMS_DISCOUNTFROMPRICE_PERCENT,  SPLITMS_DISCOUNTFROMPRICE_SUM )
      VALUES( :SPLITMS_ID,  :DISCOUNTTYPE_ID,  :SPLITMS_DISCOUNTFROMPRICE_PER,  :SPLITMS_DISCOUNTFROMPRICE_SUM );
    END


  RETURN_VALUE = 0;

  suspend;
END
 ~~~
SET TERM ; ~~~
commit work;

