CREATE TABLE [dbo].[HCACTIVIPCE] (
    [CODACTPCE] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMACTPCE] VARCHAR (150) NOT NULL,
    [ESTADO]    INT           NULL,
    [EMINANIOS] INT           NOT NULL,
    [EMINMESES] INT           NOT NULL,
    [EMINDIAS]  INT           NOT NULL,
    [EMAXANIOS] INT           NOT NULL,
    [EMAXMESES] INT           NOT NULL,
    [EMAXDIAS]  INT           NOT NULL,
    [IPSEXO]    CHAR (1)      NOT NULL,
    [TIPOACT]   INT           NULL,
    [APLICAROL] INT           NULL,
    CONSTRAINT [PK_HCACTIVIPCE] PRIMARY KEY CLUSTERED ([CODACTPCE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'True = Activo   False = Inactivo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';

