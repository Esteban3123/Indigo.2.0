CREATE TABLE [dbo].[HCMOTLIB] (
    [ID]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODMOTLIB] CHAR (3)      NOT NULL,
    [DESMOTLIB] VARCHAR (200) NOT NULL,
    [ESTMOTLIB] BIT           NOT NULL,
    CONSTRAINT [PK_HCMOTLIB] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Motivo de Liberacion  1- activo  0- inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTLIB', @level2type = N'COLUMN', @level2name = N'ESTMOTLIB';

