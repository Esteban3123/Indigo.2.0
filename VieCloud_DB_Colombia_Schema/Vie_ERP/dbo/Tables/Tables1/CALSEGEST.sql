CREATE TABLE [dbo].[CALSEGEST] (
    [ID]              INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCALREPORTE]    INT       NOT NULL,
    [ESTADO]          INT       NOT NULL,
    [FECHAREGISTRO]   DATETIME  NOT NULL,
    [USUARIOREGISTRO] CHAR (20) NOT NULL,
    CONSTRAINT [PK_CALSEGEST] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALSEGEST_CALREPORTE] FOREIGN KEY ([IDCALREPORTE]) REFERENCES [dbo].[CALREPORTE] ([ID]),
    CONSTRAINT [FK_CALSEGEST_SEGusuaru] FOREIGN KEY ([USUARIOREGISTRO]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[CALSEGEST] NOCHECK CONSTRAINT [FK_CALSEGEST_CALREPORTE];




GO
ALTER TABLE [dbo].[CALSEGEST] NOCHECK CONSTRAINT [FK_CALSEGEST_CALREPORTE];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario profesional (FK a SEGusuaru.CODUSUARI) que efectuó el registro y cambio de estado en el seguimiento; identificador del operador de auditoría PII para trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que realizó el Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se registró el evento de cambio de estado en el seguimiento de gestión de calidad; timestamp de auditoría del movimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código numérico del estado de gestión del reporte: 1=Reportado (ingresado inicialmente), 2=Descartado (rechazado/anulado), 3=Modificado (actualización aplicada), 4=Visado (aprobado/validado por autoridad); rastrea el flujo de seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado al que pasa el registro:     1=Reportado    2=Descartado    3=Modificado    4=Visado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de referencia a la tabla CALREPORTE (FK); vincula el evento de seguimiento y gestión al reporte de calidad original que se está supervisando.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla CALREPORTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) de la tabla CALSEGEST; clave primaria que rastrea cada evento de seguimiento de gestión de calidad en reportes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumérico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del historial de estados (seguimiento) de los reportes de calidad. Cada fila representa un cambio de estado de un reporte, con el usuario y la fecha en que se realizó el cambio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST';
