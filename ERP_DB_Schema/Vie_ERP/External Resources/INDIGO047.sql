CREATE EXTERNAL DATA SOURCE [INDIGO047]
    WITH (
    TYPE = RDBMS,
    LOCATION = N'ssindigo.database.windows.net',
    DATABASE_NAME = N'INDIGO047',
    CREDENTIAL = [LoginExternalTables]
    );

