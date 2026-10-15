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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL  Para Qx Se especifica el codigo del profesional cirujano', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';

