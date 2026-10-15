CREATE TABLE [WebAppointment].[RecommendedServices] (
    [ActivityCode]          CHAR (3)  NOT NULL,
    [IPSServiceCode]        CHAR (20) NOT NULL,
    [ContractDescriptionId] INT       NULL,
    [Position]              INT       NOT NULL,
    [SpecialtyCode]         CHAR (3)  NULL,
    [ProcedureType]         TINYINT   NULL,
    [ServiceType]           INT       NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1-> Consultas 2-> Procedimientos', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'ServiceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1-> Laboratorio 2-> Imagenes Diagnosticas 3-> Otros Procedimientos', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'ProcedureType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la tabla Contract.ContractDescriptions', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CODSERIPS de la tabla dbo.INCUPSIPS', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'IPSServiceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CODACTMED de la tabla dbo.AGACTIMED', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'RecommendedServices', @level2type = N'COLUMN', @level2name = N'ActivityCode';

