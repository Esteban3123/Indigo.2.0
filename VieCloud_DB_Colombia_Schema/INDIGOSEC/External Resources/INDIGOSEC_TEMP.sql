CREATE EXTERNAL DATA SOURCE [INDIGOSEC_TEMP]
    WITH (
    TYPE = RDBMS,
    LOCATION = N'ssindigodev.database.windows.net',
    DATABASE_NAME = N'INDIGOSEC_TEMP',
    CREDENTIAL = [LoginExternalTables]
    );

