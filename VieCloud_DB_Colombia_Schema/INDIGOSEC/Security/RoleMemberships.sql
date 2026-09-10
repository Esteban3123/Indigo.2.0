ALTER ROLE [db_ddladmin] ADD MEMBER [LoginExternalTables];


GO
ALTER ROLE [db_datareader] ADD MEMBER [LoginExternalTables];


GO
ALTER ROLE [db_datareader] ADD MEMBER [IPC-SQL-Application];


GO
ALTER ROLE [db_datareader] ADD MEMBER [mconde];


GO
ALTER ROLE [db_datareader] ADD MEMBER [mcalderon];


GO
ALTER ROLE [db_datareader] ADD MEMBER [IPC-SQL-DEV-Application];


GO
ALTER ROLE [db_datareader] ADD MEMBER [IPC-SQL-Developers];


GO
ALTER ROLE [db_datareader] ADD MEMBER [IPC-SQL-DevelopersQA];


GO
ALTER ROLE [db_datawriter] ADD MEMBER [IPC-SQL-Application];


GO
ALTER ROLE [db_datawriter] ADD MEMBER [IPC-SQL-DEV-Application];


GO
ALTER ROLE [db_datawriter] ADD MEMBER [IPC-SQL-Developers];


GO
ALTER ROLE [db_datawriter] ADD MEMBER [IPC-SQL-DevelopersQA];

