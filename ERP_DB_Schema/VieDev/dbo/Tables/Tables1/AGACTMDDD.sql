CREATE TABLE [dbo].[AGACTMDDD] (
    [AUTONUMER]                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODACTMED]                CHAR (3)        NOT NULL,
    [CODSERIPS]                CHAR (20)       NOT NULL,
    [TIPOPARAM]                NCHAR (1)       NOT NULL,
    [CONCECREC]                INT             NULL,
    [CODPRODUC]                CHAR (20)       NULL,
    [CANTRECUR]                INT             NULL,
    [REACALAUT]                BIT             NULL,
    [MEDIOCONT]                BIT             NULL,
    [APLRANEDA]                BIT             NULL,
    [MLPRODUCT]                NUMERIC (10, 2) NULL,
    [KGPRODUCT]                NUMERIC (10, 2) NULL,
    [EDADESDE]                 TINYINT         NULL,
    [EDAHASTA]                 TINYINT         NULL,
    [PRODAUTON]                INT             NULL,
    [IDDESCRIPCIONRELACIONADA] INT             NULL,
    CONSTRAINT [PK_AGACTMDDD] PRIMARY KEY CLUSTERED ([AUTONUMER] ASC),
    CONSTRAINT [FK_AGACTMDDD_AGERECURS] FOREIGN KEY ([CONCECREC]) REFERENCES [dbo].[AGERECURS] ([CODCONCEC]),
    CONSTRAINT [FK_AGACTMDDD_SOLPRODUC] FOREIGN KEY ([PRODAUTON]) REFERENCES [dbo].[SOLPRODUC] ([PRODAUTON])
);


GO
ALTER TABLE [dbo].[AGACTMDDD] NOCHECK CONSTRAINT [FK_AGACTMDDD_AGERECURS];


GO
ALTER TABLE [dbo].[AGACTMDDD] NOCHECK CONSTRAINT [FK_AGACTMDDD_SOLPRODUC];

