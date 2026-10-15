CREATE TABLE [dbo].[HCLISTEXCE] (
    [AUTO]       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONSECC] INT       NOT NULL,
    [UFUCODIGO]  CHAR (10) NOT NULL,
    CONSTRAINT [PK_HCLISTEXCE] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCLISTEXCE_HCLISTACC] FOREIGN KEY ([CODCONSECC]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC])
);


GO
ALTER TABLE [dbo].[HCLISTEXCE] NOCHECK CONSTRAINT [FK_HCLISTEXCE_HCLISTACC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional aplicar Excepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTEXCE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la Cabecera de la Lista de Chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTEXCE', @level2type = N'COLUMN', @level2name = N'CODCONSECC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTEXCE', @level2type = N'COLUMN', @level2name = N'AUTO';

