CREATE EXTERNAL DATA SOURCE [INDIGO045]
    WITH (
    TYPE = RDBMS,
    LOCATION = N'ssindigo.database.windows.net',
    DATABASE_NAME = N'INDIGO045',
    CREDENTIAL = [LoginExternalTables]
    );

