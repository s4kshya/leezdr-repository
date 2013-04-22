--------------------------------------------------------
--  File created - Friday-April-12-2013   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table DASH_PARAM
--------------------------------------------------------


Insert into CS5226.DASH_PARAM (PARAM,GREEN_S,GREEN_E,YELLOW_S,YELLOW_E,RED_S,RED_E,"DESC") values ('SP',0,59.99,60,63,63.01,100,'Shared Pool');
Insert into CS5226.DASH_PARAM (PARAM,GREEN_S,GREEN_E,YELLOW_S,YELLOW_E,RED_S,RED_E,"DESC") values ('RB',0,0.19,0.2,0.5,0.51,1,'Redo Log Buffer');
Insert into CS5226.DASH_PARAM (PARAM,GREEN_S,GREEN_E,YELLOW_S,YELLOW_E,RED_S,RED_E,"DESC") values ('BC',99.01,100,90,99,0,89.99,'Buffer Cache');
Insert into CS5226.DASH_PARAM (PARAM,GREEN_S,GREEN_E,YELLOW_S,YELLOW_E,RED_S,RED_E,"DESC") values ('SORT',90.01,100,10,90,0,9.99,'Memory area used for Sorting');

--------------------------------------------------------
--  File created - Friday-April-12-2013   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table DASH_SQL
--------------------------------------------------------


Insert into CS5226.DASH_SQL (PARAM,SQL,ADVICE,"DESC") values ('BC','select
   size_for_estimate as "Size for Est (MB)",
   size_factor as "Size Factor",
   buffers_for_estimate as "Buffers (thousands)",
   estd_physical_read_factor as "Est Phys Read Factor",
   estd_physical_reads as "Est Phys Reads (thousands)",
   estd_physical_read_time as "Est Phys Read Time",
   estd_pct_of_db_time_for_reads as "Est DBtime for Rds"
from v$db_cache_advice
where name = ''DEFAULT''
and advice_status = ''ON''
and block_size  = (SELECT value FROM V$PARAMETER WHERE name = ''db_block_size'')','Suggest to change db_cache_size or sga_target (if using ASMM mode) or memory_target (if using AMM mode) in init.ora, PFILE, SPFILE or Memory.','Buffer Cache');
Insert into CS5226.DASH_SQL (PARAM,SQL,ADVICE,"DESC") values ('SP','SELECT
   shared_pool_size_for_estimate as "Share Pool Size (MB)", 
   shared_pool_size_factor as "SP Size Factor", 
   estd_lc_size as "Est LC Size (MB)", 
   estd_lc_memory_objects as "Est LC Memory Obj", 
   estd_lc_time_saved as "Est LC Time Saved (s)", 
   estd_lc_time_saved_factor as "Est LC Time Saved Factor", 
   estd_lc_load_time as "Est LC Load Time (s)",
   estd_lc_load_time_factor as "Est LC Load Time Factor",
   estd_lc_memory_object_hits as "Est LC Memory Obj Hits (K)"
FROM
   v$shared_pool_advice','Suggest to change share_pool_size or sga_target (if using ASMM mode) or memory_target (if using AMM mode) in init.ora, PFILE, SPFILE or Memory. ','Shared Pool');
Insert into CS5226.DASH_SQL (PARAM,SQL,ADVICE,"DESC") values ('RB','select optimal_logfile_size from V$INSTANCE_RECOVERY',' Suggest to switch current used logfile to INACTIVE. Drop and re-create logfile as recommended size.','Redo Log Buffer');
Insert into CS5226.DASH_SQL (PARAM,SQL,ADVICE,"DESC") values ('SORT','select 
  round(pga_target_for_estimate/1024/1024, 0) as "SGA Target Size (MB)", 
  pga_target_factor as "SGA Size Factor", 
  estd_pga_cache_hit_percentage as "Est DB Time (s)", 
  estd_time as "Est Physical Reads"
from v$pga_target_advice','Suggest to change sort_area_size or pga_aggregate_target (if using Automatic PGA Memory Management mode) or memory_target (if using AMM mode) in init.ora, PFILE, SPFILE or Memory.','Memory area used for Sorting');

--------------------------------------------------------
--  File created - Friday-April-12-2013   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table DASH_CONFIG
--------------------------------------------------------


Insert into SYS.DASH_CONFIG (K,KVAL) values ('XVAL','60');
Insert into SYS.DASH_CONFIG (K,KVAL) values ('YVAL','10');
