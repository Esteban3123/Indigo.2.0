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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id de codigo motivos de anulación', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'IDCODMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la descripción logotipo de enrutamiento', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Código de usuario', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ruta de destino  0- Pendiente  1- Farmacia  2- Central de mezclas', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'RoutingTo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ruta de Origen :  0- Pendiente  1- Farmacia  2- Central de mezclas', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'RoutingFrom';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del Log de enrutamiento', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id relación detalle de farmacia (HCFARMEPD)', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'IDHCFARMEPD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'RoutingLog', @level2type = N'COLUMN', @level2name = N'Id';

