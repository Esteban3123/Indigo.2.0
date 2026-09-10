CREATE TABLE [MedicalHistory].[RoutingLog] (
    [Id]           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCFARMEPD]  INT           NOT NULL,
    [CreationDate] DATETIME      NOT NULL,
    [RoutingFrom]  TINYINT       NOT NULL,
    [RoutingTo]    TINYINT       NOT NULL,
    [UserCode]     CHAR (20)     NOT NULL,
    [Description]  VARCHAR (500) NULL,
    [IDCODMOTANU]  CHAR (4)      NULL,
    CONSTRAINT [PK_RoutingLog] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RoutingLog_HCFARMEPD] FOREIGN KEY ([IDHCFARMEPD]) REFERENCES [dbo].[HCFARMEPD] ([ID])
);


GO
ALTER TABLE [MedicalHistory].[RoutingLog] NOCHECK CONSTRAINT [FK_RoutingLog_HCFARMEPD];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de motivo de anulación (CHAR 4, PII); almacena la razón por la cual se anuló o rechazó la prescripción o despacho; referencia a tabla de motivos (glosa, cambio médico, etc.).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'IDCODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id de codigo motivos de anulación', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'IDCODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'IDCODMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual (VARCHAR 500) del evento o movimiento de enrutamiento; detalla el contexto o motivo del cambio de estado/destino en el flujo farmacéutico.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la descripción logotipo de enrutamiento', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación (CHAR 20) del usuario/profesional de salud (farmacéutico, técnico, auxiliar) que realizó o registró el enrutamiento.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Código de usuario', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad o punto de destino del enrutamiento: 0=Pendiente, 1=Farmacia, 2=Central de mezclas; indica adónde se envía o transfiere el medicamento o receta.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'RoutingTo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ruta de destino  0- Pendiente  1- Farmacia  2- Central de mezclas', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'RoutingTo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'RoutingTo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad o punto de origen del enrutamiento: 0=Pendiente, 1=Farmacia, 2=Central de mezclas; indica dónde se originó el movimiento del medicamento o receta.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'RoutingFrom';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ruta de Origen :  0- Pendiente  1- Farmacia  2- Central de mezclas', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'RoutingFrom';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'RoutingFrom';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro de enrutamiento; marca cuándo se registró el movimiento de la receta o medicamento en el flujo.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del Log de enrutamiento', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia el detalle de la orden/prescripción farmacéutica en HCFARMEPD; vincula el log al registro de medicamento dispensado.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'IDHCFARMEPD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id relación detalle de farmacia (HCFARMEPD)', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'IDHCFARMEPD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'IDHCFARMEPD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y consecutivo (IDENTITY) del registro de enrutamiento en el log de farmacia.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de enrutamiento o traslado de fórmulas/epícrisis farmacéuticas en la historia clínica. Guarda el historial de cada cambio de estado o redireccionamiento de un documento farmacéutico, indicando quién lo realizó, desde dónde y hacia dónde fue enviado, y el motivo de la anulación si aplica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog';
