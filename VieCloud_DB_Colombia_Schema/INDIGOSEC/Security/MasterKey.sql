-- Clave maestra de la base de datos (Azure SQL: sin password, protegida por el
-- service master key del servicio). Requerida por la credencial de ámbito de
-- base de datos [LoginExternalTables] usada por los external data sources de
-- elastic query (SQL71589).
CREATE MASTER KEY;
