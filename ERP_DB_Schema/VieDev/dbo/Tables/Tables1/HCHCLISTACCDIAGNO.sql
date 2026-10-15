CREATE TABLE [dbo].[HCHCLISTACCDIAGNO] (
    [ID]          INT      IDENTITY (1, 1) NOT NULL,
    [IDHCLISTACC] INT      NOT NULL,
    [DIAGCODIGO]  CHAR (4) NOT NULL,
    CONSTRAINT [PK_HCHCLISTACCDIAGNO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCHCLISTACCDIAGNO_HCLISTACC] FOREIGN KEY ([IDHCLISTACC]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC]),
    CONSTRAINT [FK_HCHCLISTACCDIAGNO_INDIAGNOS] FOREIGN KEY ([DIAGCODIGO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[HCHCLISTACCDIAGNO] NOCHECK CONSTRAINT [FK_HCHCLISTACCDIAGNO_HCLISTACC];


GO
ALTER TABLE [dbo].[HCHCLISTACCDIAGNO] NOCHECK CONSTRAINT [FK_HCHCLISTACCDIAGNO_INDIAGNOS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relación con el código de los diagnósticos - tabla INDIAGNOS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHCLISTACCDIAGNO', @level2type = N'COLUMN', @level2name = N'DIAGCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relación con la tabla cabecera - HCLISTACC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHCLISTACCDIAGNO', @level2type = N'COLUMN', @level2name = N'IDHCLISTACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHCLISTACCDIAGNO', @level2type = N'COLUMN', @level2name = N'ID';

