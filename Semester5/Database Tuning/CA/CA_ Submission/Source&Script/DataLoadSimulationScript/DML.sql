DECLARE
  customer_id   NUMBER;
  item_id       NUMBER;
  order_id      NUMBER;
  item_order_id NUMBER;
  totalprice    NUMBER;
  thisprice	Number;
  CURSOR c1
  IS
    SELECT SUM(item.price) totalprice
    FROM order_item,
      orders,
      item
    WHERE orders.id        = order_item.order_id
    AND order_item.item_id = item.id
    GROUP BY orders.id,
      order_item.item_id;
      
CURSOR c2
  IS
    SELECT order_item.description d1, item.description d2, orders.description d3 
    FROM order_item,
      orders,
      item
    WHERE orders.id        = order_item.order_id
    AND order_item.item_id = item.id
    and order_item.description like 'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx%'
	order by order_item.description;

BEGIN
  dbms_output.enable;
  delete * from customer;
  delete * from orders;
  delete * from item;
  delete * from order_item;
  commit;

  FOR customer_id IN 0..99
  LOOP
    INSERT
    INTO customer VALUES
      (
        'customer_'
        || customer_id,
        'customer_name_'
        || customer_id,
         dbms_random.string('A', 20)
      );
    COMMIT;
  END LOOP;
  FOR order_id IN 0..20000
  LOOP
    INSERT
    INTO orders VALUES
      (
        'order_'
        || order_id,
        'customer_'
        || ROUND (dbms_random.value(0,99)),
        sysdate,
         dbms_random.string('A', 20)
      );
    IF remainder(order_id, 10000) = 0 THEN
      dbms_output.put_line ('commit order at ' || order_id);
      COMMIT;
    END IF;
  END LOOP;
  COMMIT;
  FOR item_id IN 0..20000
  LOOP
    INSERT
    INTO item VALUES
      (
        'item_'
        || item_id,
         dbms_random.string('A', 20),
        ROUND (dbms_random.value(1,999))
      );
    IF remainder(item_id, 10000) = 0 THEN
      dbms_output.put_line ('commit item at ' || item_id);
      COMMIT;
    END IF;
  END LOOP;
  COMMIT;
  FOR item_order_id IN 0..100000
  LOOP
    INSERT
    INTO order_item VALUES
      (
        'order_'
        || ROUND (dbms_random.value(0,20000)),
        'item_'
        || ROUND (dbms_random.value(0,20000)),
        ROUND (dbms_random.value(1,999)),
        '', '','',''
      );
    IF remainder(item_order_id, 10000) = 0 THEN
      dbms_output.put_line ('commit order_item at ' || item_order_id);
      COMMIT;
    END IF;
  END LOOP;
  COMMIT;
  update item_order set description = 'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx' || dbms_random.string('A', 20) , description1 =  dbms_random.string('A', 200), description2 =  dbms_random.string('A', 400),description3 =  dbms_random.string('A', 600);
  commit;
  
  FOR record1 in c1
   LOOP
      dbms_output.put_line ('totalprice ' || record1.totalprice);
   END LOOP;
 
  FOR record2 in c1
   LOOP
      dbms_output.put_line ('desc ' || record2.d1);
   END LOOP;
   
END; 