CREATE TABLE [dbo].[HCPARCONO] (
    [CODVIAADM]                CHAR (3)        NOT NULL,
    [DESVIAADM]                CHAR (50)       NOT NULL,
    [RANGMINIM]                DECIMAL (18, 2) NULL,
    [RANGMAXIM]                DECIMAL (18, 2) NULL,
    [ESTADO]                   BIT             NOT NULL,
    [CODSERIPS]                CHAR (20)       NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT             NULL,
    [GasType]                  INT             NULL,
    CONSTRAINT [PK_HCPARCONO] PRIMARY KEY CLUSTERED ([CODVIAADM] ASC),
    CONSTRAINT [FK_HCPARCONO_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[HCPARCONO] NOCHECK CONSTRAINT [FK_HCPARCONO_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que almacena el  Tipo de gas    1 - Oxigeno  2 - Oxido Nititico  3 - Oxihelio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'GasType';

