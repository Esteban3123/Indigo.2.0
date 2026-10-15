CREATE TABLE [dbo].[SOLMARCA] (
    [Autonumerico] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Codigo]       VARCHAR (4)   NOT NULL,
    [Descripcion]  VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_SOLMARCA] PRIMARY KEY CLUSTERED ([Autonumerico] ASC)
);

