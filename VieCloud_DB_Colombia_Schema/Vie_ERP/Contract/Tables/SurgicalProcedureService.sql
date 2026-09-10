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



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SurgicalProcedureService__IPSServiceParentId__IPSServiceId]
    ON [Contract].[SurgicalProcedureService]([IPSServiceParentId] ASC, [IPSServiceId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico cirujano, anestesiólogo, enfermera instrumentista) que realiza o administra el servicio quirúrgico. Tipo CHAR(20), PII, obtenido de tabla INPROFSAL. Para procedimientos Qx especifica código del cirujano responsable.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL  Para Qx Se especifica el codigo del profesional cirujano', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que especifica si este servicio quirúrgico es el registro por defecto en el contrato. Valor 1=servicio predeterminado, 0=no predeterminado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'DefaultService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el registro que va por defecto', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'DefaultService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'DefaultService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica (INT) del servicio quirúrgico: número de unidades, sesiones o procedimientos incluidos en el contrato o facturación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'ServiceAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del servicio', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'ServiceAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'ServiceAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del servicio IPS componente o detalle que integra el procedimiento quirúrgico padre. Referencia a [Contract].[IPSService].', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio ips el cual compone al servicio quirurgico', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del servicio padre o procedimiento quirúrgico principal que agrupa y contiene servicios componentes. Referencia a [Contract].[IPSService].', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'IPSServiceParentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio padre, es decir el procedimiento quirurgico que contiene a los demas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'IPSServiceParentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'IPSServiceParentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK IDENTITY) del registro de servicio quirúrgico en el contrato. Clave primaria clustered para relación de servicios padre-hijo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio del procedimiento quirurgico', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona procedimientos quirúrgicos con los servicios o insumos asociados dentro de un contrato, indicando cantidades, si es el servicio predeterminado y el profesional de salud que lo realiza.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalProcedureService';
