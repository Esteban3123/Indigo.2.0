CREATE TABLE [dbo].[INTERERRO] (
    [ORDEN_INDIGO] VARCHAR (20)  NOT NULL,
    [CODSERIPS]    VARCHAR (15)  NOT NULL,
    [OBSERVACION]  VARCHAR (250) NULL,
    CONSTRAINT [PK_INTERERRO] PRIMARY KEY CLUSTERED ([ORDEN_INDIGO] ASC, [CODSERIPS] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion Error Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERERRO', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERERRO', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Oder de Servicio Concatenado codigo Paciente y Nuemero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERERRO', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';

