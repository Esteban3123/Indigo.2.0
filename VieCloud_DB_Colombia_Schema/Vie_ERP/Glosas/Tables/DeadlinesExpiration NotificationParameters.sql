CREATE TABLE [Glosas].[DeadlinesExpiration NotificationParameters] (
    [Id]                TINYINT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdTimeParameters]  TINYINT       NOT NULL,
    [NotificationType]  CHAR (1)      NOT NULL,
    [NotificationValue] VARCHAR (100) NOT NULL,
    [TimeStamp]         ROWVERSION    NOT NULL,
    CONSTRAINT [PK_DeadlinesExpiration NotificationParameters] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DeadlinesExpiration NotificationParameters_TimeParameters] FOREIGN KEY ([IdTimeParameters]) REFERENCES [Glosas].[TimeParameters] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP SQL Server) del evento de notificación de vencimiento de glosa. Registra el instante exacto de creación, modificación o procesamiento del parámetro de notificación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o contenido de la notificación de vencimiento de glosa (VARCHAR 100). Puede contener texto del mensaje, destinatario, umbral de días o parámetro específico según el tipo de notificación (SMS/SMTP).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters', @level2type = N'COLUMN', @level2name = N'NotificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de notificación', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters', @level2type = N'COLUMN', @level2name = N'NotificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters', @level2type = N'COLUMN', @level2name = N'NotificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de canal de notificación para alertas de vencimiento de glosa: S=SMS, E=SMTP/Correo electrónico. Determina el medio de comunicación (celular o email) del aviso de deadline de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters', @level2type = N'COLUMN', @level2name = N'NotificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - SMS, 2 - SMTP', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters', @level2type = N'COLUMN', @level2name = N'NotificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters', @level2type = N'COLUMN', @level2name = N'NotificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (TINYINT, FK) que referencia la tabla TimeParameters. Vincula este parámetro de notificación con la configuración temporal (días, horas) de vencimiento de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters', @level2type = N'COLUMN', @level2name = N'IdTimeParameters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Parámetros de tiempo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters', @level2type = N'COLUMN', @level2name = N'IdTimeParameters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters', @level2type = N'COLUMN', @level2name = N'IdTimeParameters';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (TINYINT IDENTITY 1,1) de la tabla. Clave primaria que identifica cada configuración de notificación de vencimiento de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de notificación para el vencimiento de plazos en el proceso de glosas: define cómo y a quién se notifica cuando un plazo de respuesta o radicación de glosa está próximo a expirar.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters';
