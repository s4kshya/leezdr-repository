BEGIN
  EXECUTE IMMEDIATE 'CREATE TABLE customer(id varchar2 (40) primary key,name varchar2 (100))';
  EXECUTE IMMEDIATE 'CREATE TABLE orders(id varchar2 (40) primary key,customer_id varchar2 (100) ,order_date date, description varchar2(2000))';
  EXECUTE IMMEDIATE 'CREATE TABLE order_item(order_id varchar2(40),item_id varchar2(40),quantity number (8), description varchar(2000), description1 varchar(2000), description2 varchar(2000), description3 varchar(2000))';
  EXECUTE IMMEDIATE 'CREATE TABLE item(id varchar2(40) primary key,price number (5,2), description varchar2(2000))';  
END; 