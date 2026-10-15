CREATE TABLE [Contract].[SurgicalProcedureService] (
    [Id]                             INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPSServiceParentId]             INT       NOT NULL,
    [IPSServiceId]                   INT       NOT NULL,
    [ServiceAmount]                  INT       NOT NULL,
    [DefaultService]                 BIT       CONSTRAINT [DF_SurgicalProcedureService_DefaultService] DEFAULT ((0)) NOT NULL,
    [PerformsHealthProfessionalCode] CHAR (20) NULL,
    CONSTRAINT [PK_SurgicalProcedureService__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SurgicalProcedureService_IPSService] FOREIGN KEY ([IPSServiceParentId]) REFERENCES [Contract].[IPSService] ([Id]),
    CONSTRAINT [FK_SurgicalProcedureService_IPSService1] FOREIGN KEY ([IPSServiceId]) REFERENCES [Contract].[IPSService] ([Id])
);


GO
ALTER TABLE [Contract].[SurgicalProcedureService] NOCHECK CONSTRAINT [FK_SurgicalProcedureService_IPSService];


GO
ALTER TABLE [Contract].[SurgicalProcedureService] NOCHECK CONSTRAINT [FK_SurgicalProcedureService_IPSService1];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SurgicalProcedureService__IPSServiceParentId__IPSServiceId]
    ON [Contract].[SurgicalProcedureService]([IPSServiceParentId] ASC, [IPSServiceId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL  Para Qx Se especifica el codigo del profesional cirujano', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el registro que va por defecto', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'DefaultService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del servicio', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'ServiceAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del servicio ips el cual compone al servicio quirurgico', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del servicio padre, es decir el procedimiento quirurgico que contiene a los demas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'IPSServiceParentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del servicio del procedimiento quirurgico', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'Id';

