/*------   ------*/

SET TERM ~~~ ;
 ALTER procedure USP_ADD_WAYBILL_FROMSQL (
    SUPPL_ID integer,
    CARDS_SHIPDATE date,
    WAYBILL_NUM varchar(16),
    WAYBILL_FORSTOCK integer)
returns (
    WAYBILL_ID integer,
    ERROR_NUMBER integer,
    ERROR_TEXT varchar(480))
as
declare variable NEW_WAYBILL_ID integer;
declare variable SUPPL_STATE integer;
declare variable WAYBILL_SRCCODE integer;
declare variable WAYBILL_SRCDOC varchar(16);
declare variable SUPPL_NUM integer;
declare variable WAYBILL_SRCID integer;
declare variable WAYBILL_SRCDATE date;
declare variable WAYBILL_BEGINDATE date;
declare variable CUSTOMER_ID integer;
declare variable COMPANY_ID integer;
declare variable CHILDCUST_ID integer;
declare variable DEPART_CODE varchar(3);
declare variable WAYBILL_ENDDATE date;
declare variable STMNT_MAXDELAY integer;
declare variable KIND_OF_DAYS integer;
declare variable STMNT_CATEGORY char(1);
declare variable STMNT_ID integer;
declare variable WAYBILL_INCASSO integer;
declare variable WAYBILL_BONUS integer;
declare variable WAYBILL_MONEYBONUS integer;
declare variable WAYBILL_CURRENCYRATE double precision;
declare variable CURRENCY_CODE varchar(3);
declare variable WAYBILL_AUTOCREATED smallint;
declare variable SHIP_MODE integer;
declare variable STOCK_ID integer;
declare variable WAYBILL_ALLPRICE double precision;
declare variable WAYBILL_AMOUNTPAID double precision;
declare variable WAYBILL_ALLDISCOUNT double precision;
declare variable WAYBILL_CURRENCYALLDISCOUNT double precision;
declare variable WAYBILL_CURRENCYALLPRICE double precision;
declare variable WAYBILL_CURRENCYAMOUNTPAID double precision;
declare variable WAYBILL_DATELASTPAID date;
declare variable WAYBILL_SHIPDATE date;
declare variable WAYBILL_USDRATE double precision;
declare variable WAYBILL_CURRENCYMAINRATE double precision;
declare variable WAYBITMS_ID integer;
declare variable PARTS_ID integer;
declare variable MEASURE_ID integer;
declare variable WAYBITMS_QUANTITY integer;
declare variable WAYBITMS_PACKID integer;
declare variable WAYBITMS_PRICE double precision;
declare variable WAYBITMS_DISCOUNT double precision;
declare variable WAYBITMS_DATELASTPAID date;
declare variable WAYBITMS_AMOUNTPAID double precision;
declare variable WAYBITMS_BASEPRICE double precision;
declare variable WAYBITMS_MARKUP double precision;
declare variable WAYBITMS_CURRENCYPRICE double precision;
declare variable WAYBITMS_CURRENCYAMOUNTPAID double precision;
declare variable WAYBITMS_NDS double precision;
declare variable SPLITMS_ID integer;
declare variable WAYBITMS_DISCOUNTPRICE double precision;
declare variable WAYBITMS_CURRENCYDISCOUNTPRICE double precision;
declare variable WAYBITMS_DISCOUNTFROMPRICE double precision;
declare variable INSTOCK_ID integer;
declare variable CARDS_OPERQTY integer;
declare variable CARDS_OPERPRICE double precision;
declare variable CARDS_CURRENTQTY integer;
declare variable CARDS_RESQTY integer;
declare variable SPLITMS_UPDATEQTY integer;
declare variable CARDS_OPERDOC varchar(16);
declare variable SPLITMS_QUANTITY integer;
declare variable SRCCARDS_CURRENTQTY integer;
declare variable SRCCARDS_RESQTY integer;
begin
  WAYBILL_ID = null;

  ERROR_NUMBER = -1;
  ERROR_TEXT = '';


  /* проверка на наличие протокола с указанным идентификатором */
  if( not exists ( select suppl_id from t_suppl suppl where suppl_id = :suppl_id ) ) then
   begin

    ERROR_NUMBER = 1;
    ERROR_TEXT = cast(('В базе данных не найден протокол с указанным кодом, либо протокол нельзя перевести в накладную. Код: ' || cast( :suppl_id as varchar(8))) as varchar(480));
    suspend;

    exit;
   end

  select suppl.stmnt_id, suppl.suppl_num, suppl.suppl_state, suppl.suppl_begindate, stmnt.customer_id, stmnt.company_id,
   suppl.childcust_id, suppl.depart_code, suppl.suppl_incasso, suppl.suppl_bonus, suppl.suppl_moneybonus,
   suppl.suppl_currencyrate, suppl.currency_code, suppl.suppl_autocreated, suppl.stock_id,
   suppl.suppl_allprice, suppl.suppl_alldiscount, suppl.suppl_currencyallprice, suppl.suppl_currencyalldiscount
  from t_suppl suppl, t_stmnt stmnt
  where suppl.suppl_id = :suppl_id
    and suppl.stmnt_id = stmnt.stmnt_id
  into :STMNT_ID, :SUPPL_NUM, :SUPPL_STATE, :WAYBILL_SRCDATE, :CUSTOMER_ID, :COMPANY_ID, :CHILDCUST_ID,
   :DEPART_CODE, :WAYBILL_INCASSO, :WAYBILL_BONUS, :WAYBILL_MONEYBONUS, :WAYBILL_CURRENCYRATE, :CURRENCY_CODE,
   :WAYBILL_AUTOCREATED, :STOCK_ID, :WAYBILL_ALLPRICE, :WAYBILL_ALLDISCOUNT,  :WAYBILL_CURRENCYALLPRICE, :WAYBILL_CURRENCYALLDISCOUNT;

  if( :SUPPL_STATE not in ( 0, 3, 4, 100 ) ) then
   begin

    ERROR_NUMBER = 2;
    ERROR_TEXT = cast(('Протокол нельзя перевести в накладную. Код состояния протокола: ' || cast( :SUPPL_STATE as varchar(8))) as varchar(480));
    suspend;

    exit;
   end

   SHIP_MODE = 0;
   WAYBILL_SRCCODE = 2;
   WAYBILL_SRCDOC = cast( :SUPPL_NUM as varchar( 16 ) );
   WAYBILL_SRCID = :suppl_id;
   WAYBILL_BEGINDATE = trimtime( 'now' );
   WAYBILL_CURRENCYMAINRATE = 1;
   WAYBILL_AMOUNTPAID = 0;
   WAYBILL_CURRENCYAMOUNTPAID = 0;
   WAYBILL_DATELASTPAID = NULL;
   WAYBILL_SHIPDATE = NULL;
   WAYBILL_USDRATE = 0;


   /* EXECUTE PROCEDURE SP_GETWAYBILLID RETURNING_VALUES :NEW_WAYBILL_ID; */
   NEW_WAYBILL_ID = GEN_ID(g_WAYBILLID, 1);

   EXECUTE PROCEDURE SP_GETSTMNTMAXDELAY( :STMNT_ID ) RETURNING_VALUES :STMNT_MAXDELAY, :KIND_OF_DAYS, :STMNT_CATEGORY;
   WAYBILL_ENDDATE = :WAYBILL_BEGINDATE + :STMNT_MAXDELAY;

   EXECUTE PROCEDURE SP_ADDWAYBILL( :SHIP_MODE, :NEW_WAYBILL_ID, :STOCK_ID, :COMPANY_ID, :CUSTOMER_ID, :DEPART_CODE,
    :WAYBILL_SRCCODE, :WAYBILL_SRCDOC, :WAYBILL_SRCID, :WAYBILL_SRCDATE, :WAYBILL_BEGINDATE, :WAYBILL_ALLPRICE,
    :WAYBILL_ENDDATE, :WAYBILL_AMOUNTPAID, :CURRENCY_CODE, :WAYBILL_CURRENCYRATE, :WAYBILL_ALLDISCOUNT,
    :WAYBILL_CURRENCYALLDISCOUNT, :WAYBILL_CURRENCYALLPRICE, :WAYBILL_CURRENCYAMOUNTPAID,
    :WAYBILL_DATELASTPAID, :WAYBILL_SHIPDATE, :WAYBILL_NUM, :CHILDCUST_ID, :WAYBILL_INCASSO, :WAYBILL_BONUS,
    :WAYBILL_USDRATE, :WAYBILL_MONEYBONUS, :WAYBILL_CURRENCYMAINRATE );

  if( not exists ( select waybill_id from t_waybill suppl where waybill_id = :NEW_WAYBILL_ID ) ) then
   begin

    ERROR_NUMBER = 3;
    ERROR_TEXT = cast(('В базе данных не удалось зарегистрировать шапку накладной. Код: ' || cast( :NEW_WAYBILL_ID as varchar(8))) as varchar(480));
    suspend;

    exit;
   end

  for select splitms.splitms_id, splitms.splitms_quantity, splitms.parts_id, splitms.measure_id, splitms.splitms_price,
   splitms.splitms_discount, splitms.splitms_baseprice, splitms.splitms_markup, splitms.splitms_currencyprice,
   splitms.splitms_nds, splitms.splitms_discountprice, splitms.splitms_currencydiscountprice,
   splitms.splitms_discountfromprice
  from t_splitms splitms where splitms.suppl_id = :suppl_id
  into :SPLITMS_ID, :WAYBITMS_QUANTITY, :parts_id, :measure_id, :WAYBITMS_PRICE,
   :WAYBITMS_DISCOUNT, :WAYBITMS_BASEPRICE, :WAYBITMS_MARKUP, :WAYBITMS_CURRENCYPRICE,
   :WAYBITMS_NDS, :WAYBITMS_DISCOUNTPRICE, :WAYBITMS_CURRENCYDISCOUNTPRICE,
   :WAYBITMS_DISCOUNTFROMPRICE
   do
    begin
     WAYBITMS_PACKID = :MEASURE_ID;
     WAYBITMS_DATELASTPAID = NULL;
     WAYBITMS_AMOUNTPAID = 0;
     WAYBITMS_CURRENCYAMOUNTPAID = 0;
     SPLITMS_QUANTITY = :WAYBITMS_QUANTITY;

     waybitms_id = GEN_ID(g_WAYBITMSID, 1);

     /* заносим общие данные */
     insert into t_waybitms (waybitms_id, waybill_id, parts_id, measure_id,
      waybitms_quantity, waybitms_packid, waybitms_price, waybitms_discount, waybitms_datelastpaid,
      waybitms_amountpaid, waybitms_baseprice, waybitms_markup,
      waybitms_currencyprice, waybitms_currencyamountpaid, waybitms_nds, splitms_id, waybitms_discountfromprice,
      waybitms_discountprice, waybitms_currencydiscountprice )
     values (:waybitms_id, :NEW_WAYBILL_ID, :parts_id, :measure_id,
      :WAYBITMS_QUANTITY, :waybitms_packid, :waybitms_price,
      :waybitms_discount, :waybitms_datelastpaid,
      :waybitms_amountpaid, :waybitms_baseprice, :waybitms_markup,
      :waybitms_currencyprice, :waybitms_currencyamountpaid, :waybitms_nds, :splitms_id, :WAYBITMS_DISCOUNTFROMPRICE,
      :waybitms_discountprice, :waybitms_currencydiscountprice );

     /* информация о скидках */
     insert into t_waybitms_discount( waybitms_id, discounttype_id, waybitms_discountfromprice_percent, waybitms_discountfromprice_sum )
     select :waybitms_id, discounttype_id, splitms_discountfromprice_percent, splitms_discountfromprice_sum
     from t_splitms_discount where splitms_id = :splitms_id;

     for select instock.instock_id, sum( -cards.cards_operqty ),
      cards.cards_operprice, instock.instock_currentqty cards_currentqty,
      instock.instock_resqty cards_resqty, cards.cards_operdoc
     from t_instock instock, t_cards cards
     where cards.cards_opercode between 8 and 9  /* учитываем и постановку в резерв и снятие с резерва */
       and cards.cards_operid = :splitms_id
       and cards.parts_id = :parts_id
       and cards.instock_id = instock.instock_id
     group by instock.instock_id, cards.cards_operprice, instock.instock_currentqty, instock.instock_resqty, cards.cards_operdoc
     into :instock_id, :cards_operqty, :cards_operprice, :cards_currentqty, :cards_resqty, :CARDS_OPERDOC
      do
       begin
        srcCARDS_CURRENTQTY = :cards_currentqty;
        srcCARDS_RESQTY = :cards_resqty;

        if ( ( splitms_quantity > 0 ) and ( cards_operqty > 0 ) ) then
         begin
          if( :splitms_quantity >= :cards_operqty ) then
           splitms_updateqty = :cards_operqty;
          else
           splitms_updateqty = :splitms_quantity;

          splitms_quantity = ( :splitms_quantity - :splitms_updateqty );

          cards_currentqty = ( :cards_currentqty + :splitms_updateqty );
          cards_resqty = cards_resqty - splitms_updateqty;
        
          insert into t_cards ( instock_id, parts_id, cards_operqty, cards_operprice, cards_currentqty, cards_resqty,
           cards_opercode, cards_operdoc, cards_operid, cards_shipdate )
          values ( :instock_id, :parts_id, :splitms_updateqty, :cards_operprice, :cards_currentqty, :cards_resqty,
           9, :CARDS_OPERDOC, :splitms_id, :cards_shipdate );


          INSERT INTO t_CARDS (instock_id, parts_id, cards_operqty, cards_operprice, cards_currentqty, cards_resqty, cards_opercode,
           cards_operdoc, cards_operid, cards_shipdate)
          VALUES (:instock_id, :parts_id, -:splitms_updateqty, :cards_operprice, :srcCARDS_CURRENTQTY, :srcCARDS_RESQTY, 15,
           :WAYBILL_NUM, :waybitms_id, :cards_shipdate);

         end /* if ( splitms_diffqty > 0 ) */
       end /* for select */

    end

    select sum( waybitms.waybitms_allprice ),
     sum( waybitms.waybitms_allprice - waybitms.waybitms_totalprice ),
     sum( waybitms.waybitms_currencyallprice ),
     sum( waybitms.waybitms_currencyallprice - waybitms.waybitms_currencytotalprice )
    from t_waybitms waybitms where waybitms.waybill_id = :NEW_WAYBILL_ID
    into :waybill_allprice,  :waybill_alldiscount, :waybill_currencyallprice, :waybill_currencyalldiscount;
    
    if( :waybill_allprice is null ) then waybill_allprice = 0;
    if( :waybill_alldiscount is null ) then waybill_alldiscount = 0;
    if( :waybill_currencyallprice is null ) then waybill_currencyallprice = 0;
    if( :waybill_currencyalldiscount is null ) then waybill_currencyalldiscount = 0;
    
    update t_waybill  set waybill_allprice = :waybill_allprice, waybill_alldiscount = :waybill_alldiscount,
     waybill_currencyallprice = :waybill_currencyallprice, waybill_currencyalldiscount = :waybill_currencyalldiscount,
     waybill_forstock = :waybill_forstock, WAYBILL_AUTOCREATED = :WAYBILL_AUTOCREATED
    where waybill_id = :NEW_WAYBILL_ID;

   /* обновляем состояние заказа */
   update t_suppl set suppl_state = 1, suppl_shipdate = sys_date() where suppl_id = :suppl_id;

  WAYBILL_ID = :NEW_WAYBILL_ID;
  ERROR_NUMBER = 0;
  ERROR_TEXT = cast(('Успешное завершение операции. Код записи: ' || cast( :WAYBILL_ID as varchar(8))) as varchar(480));

  suspend;

  when any do
   begin
    ERROR_NUMBER = -1;
    ERROR_TEXT = cast((:ERROR_TEXT || ' Не удалось создать накладную. Неизвестная ошибка, т.к не удается вернуть SQLCODE.') as varchar(480));

    suspend;
   end
end
 ~~~
SET TERM ; ~~~
commit work;



/*------   ------*/

SET TERM ~~~ ;
 ALTER procedure USP_GETWAYBITMSINFO_BYID_FROMSQL (
    IN_WAYBILL_ID integer)
returns (
    WAYBITMS_ID integer,
    WAYBILL_ID integer,
    PARTS_ID integer,
    MEASURE_ID integer,
    WAYBITMS_QUANTITY integer,
    WAYBITMS_PACKID integer,
    WAYBITMS_PRICE double precision,
    WAYBITMS_DATELASTPAID date,
    WAYBITMS_AMOUNTPAID double precision,
    WAYBITMS_BASEPRICE double precision,
    WAYBITMS_MARKUP double precision,
    WAYBITMS_DISCOUNT double precision,
    WAYBITMS_RETQTY integer,
    WAYBITMS_ALLPRICE double precision,
    WAYBITMS_DISCOUNTPRICE double precision,
    WAYBITMS_TOTALPRICE double precision,
    WAYBITMS_LEAVQTY integer,
    WAYBITMS_LEAVTOTALPRICE double precision,
    WAYBITMS_CURRENCYPRICE double precision,
    WAYBITMS_CURRENCYAMOUNTPAID double precision,
    WAYBITMS_CURRENCYALLPRICE double precision,
    WAYBITMS_CURRENCYDISCOUNTPRICE double precision,
    WAYBITMS_CURRENCYTOTALPRICE double precision,
    WAYBITMS_CURRENCYLEAVTOTALPRICE double precision,
    WAYBITMS_NDS double precision,
    WAYBITMS_DISCOUNTFROMPRICE double precision,
    SPLITMS_ID integer)
as
BEGIN

  FOR SELECT WAYBITMS_ID, WAYBILL_ID, PARTS_ID, MEASURE_ID, WAYBITMS_QUANTITY,
    WAYBITMS_PACKID, WAYBITMS_PRICE, WAYBITMS_DATELASTPAID, WAYBITMS_AMOUNTPAID,
    WAYBITMS_BASEPRICE, WAYBITMS_MARKUP, WAYBITMS_DISCOUNT, WAYBITMS_RETQTY,
    WAYBITMS_ALLPRICE, WAYBITMS_DISCOUNTPRICE, WAYBITMS_TOTALPRICE, WAYBITMS_LEAVQTY,
    WAYBITMS_LEAVTOTALPRICE, WAYBITMS_CURRENCYPRICE, WAYBITMS_CURRENCYAMOUNTPAID,
    WAYBITMS_CURRENCYALLPRICE, WAYBITMS_CURRENCYDISCOUNTPRICE, WAYBITMS_CURRENCYTOTALPRICE,
    WAYBITMS_CURRENCYLEAVTOTALPRICE, WAYBITMS_NDS, WAYBITMS_DISCOUNTFROMPRICE, SPLITMS_ID
  FROM t_waybitms waybitms
  WHERE waybitms.waybill_id = :IN_WAYBILL_ID
  into :WAYBITMS_ID, :WAYBILL_ID, :PARTS_ID, :MEASURE_ID, :WAYBITMS_QUANTITY,
    :WAYBITMS_PACKID, :WAYBITMS_PRICE, :WAYBITMS_DATELASTPAID, :WAYBITMS_AMOUNTPAID,
    :WAYBITMS_BASEPRICE, :WAYBITMS_MARKUP, :WAYBITMS_DISCOUNT, :WAYBITMS_RETQTY,
    :WAYBITMS_ALLPRICE, :WAYBITMS_DISCOUNTPRICE, :WAYBITMS_TOTALPRICE, :WAYBITMS_LEAVQTY,
    :WAYBITMS_LEAVTOTALPRICE, :WAYBITMS_CURRENCYPRICE, :WAYBITMS_CURRENCYAMOUNTPAID,
    :WAYBITMS_CURRENCYALLPRICE, :WAYBITMS_CURRENCYDISCOUNTPRICE, :WAYBITMS_CURRENCYTOTALPRICE,
    :WAYBITMS_CURRENCYLEAVTOTALPRICE, :WAYBITMS_NDS, :WAYBITMS_DISCOUNTFROMPRICE, :SPLITMS_ID
  DO
   suspend;

END
 ~~~
SET TERM ; ~~~
commit work;





