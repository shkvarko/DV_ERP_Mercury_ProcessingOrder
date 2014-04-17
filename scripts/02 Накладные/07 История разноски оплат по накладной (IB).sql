/*------   ------*/

SET TERM ~~~ ;
 create procedure USP_GETPAYMENTHISTORY (
    WAYBILL_ID integer)
returns (
    PAYMENTS_ID INTEGER,
    BANKDATE DATE,
    PAYMENTS_OPERDATE DATE,
    PAYMENTS_VALUE DOUBLE PRECISION,
    CURRENCY_CODE varchar(3),
    EARNING_ID integer,
    CUSTOMER_ID integer,
    CUSTOMER_NAME varchar(100),
    EARNING_DATE date,
    EARNING_VALUE double precision,
    EARNING_EXPENSE double precision,
    EARNING_SALDO double precision,
    COMPANY_ID integer,
    COMPANY_ACRONYM varchar(8),
    EARNING_DESCRIPTION varchar(56)
)
as
BEGIN
 for select payments.payments_id, payments.bankdate, payments.payments_operdate,
  payments.payments_value, payments.earning_id
 from t_payments payments
 where payments.payments_srcid = :WAYBILL_ID
   and payments.payments_paymentscode = 2 /* оплата и сторно по накладным */
   and payments.earning_id is not null
   and payments.earning_id <> 0
 into :payments_id, :bankdate, :payments_operdate, :payments_value, :earning_id
 do
  begin

   select earning.customer_id, customer.customer_name,
    earning.earning_date, earning.earning_value, earning.earning_expense, earning.earning_saldo,
    earning.company_id, company.company_acronym, earning.currency_code
   FROM T_EARNING earning, T_CUSTOMER customer, t_company company
   WHERE earning.earning_id =  :EARNING_ID
   and earning.customer_id = customer.customer_id
   and earning.company_id = company.company_id
   into :customer_id, :customer_name, :earning_date, :earning_value, :earning_expense,
    :earning_saldo, :company_id, :company_acronym, :currency_code;

   EARNING_DESCRIPTION = '';

   suspend;
  end

END
 ~~~
SET TERM ; ~~~
commit work;


