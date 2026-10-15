CREATE TABLE [dbo].[INCONSECUPAIS] (
    [ID]          INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPAIS]      INT         NOT NULL,
    [CONSECUTIVO] VARCHAR (6) NOT NULL,
    CONSTRAINT [PK_INCONSECUPAIS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECUPAIS', @level2type = N'COLUMN', @level2name = N'CONSECUTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que me relaciona el ID pais con la tabla de Paises ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECUPAIS', @level2type = N'COLUMN', @level2name = N'IDPAIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECUPAIS', @level2type = N'COLUMN', @level2name = N'ID';

