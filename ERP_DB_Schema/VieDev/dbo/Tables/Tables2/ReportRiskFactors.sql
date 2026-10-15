CREATE TABLE [dbo].[ReportRiskFactors] (
    [Id]                           INT           IDENTITY (1, 1) NOT NULL,
    [IPCODPACI]                    VARCHAR (25)  NOT NULL,
    [IdRiskFactor]                 INT           NOT NULL,
    [Intervention]                 VARCHAR (600) NOT NULL,
    [Observation]                  VARCHAR (500) NOT NULL,
    [UserCreation]                 CHAR (20)     NOT NULL,
    [DateCreation]                 DATETIME      NOT NULL,
    [Status]                       INT           NULL,
    [LastActivationProfessional]   CHAR (20)     NULL,
    [LastActivationDate]           DATETIME      NULL,
    [LastInactivationProfessional] CHAR (20)     NULL,
    [LastInactivationDate]         DATETIME      NULL,
    [CancellationProfessionalCode] CHAR (20)     NULL,
    [CancellationReasonCode]       CHAR (4)      NULL,
    [CancellationObservation]      VARCHAR (500) NULL,
    [CancellationDate]             DATETIME      NULL,
    CONSTRAINT [PK_ReportRiskFactors] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReportRiskFactors_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_ReportRiskFactors_RiskFactor] FOREIGN KEY ([IdRiskFactor]) REFERENCES [dbo].[RiskFactor] ([Id]),
    CONSTRAINT [FK_ReportRiskFactors_SEGusuaru_1] FOREIGN KEY ([UserCreation]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_ReportRiskFactors_SEGusuaru_2] FOREIGN KEY ([LastActivationProfessional]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[ReportRiskFactors] NOCHECK CONSTRAINT [FK_ReportRiskFactors_INPACIENT];


GO
ALTER TABLE [dbo].[ReportRiskFactors] NOCHECK CONSTRAINT [FK_ReportRiskFactors_RiskFactor];


GO
ALTER TABLE [dbo].[ReportRiskFactors] NOCHECK CONSTRAINT [FK_ReportRiskFactors_SEGusuaru_1];


GO
ALTER TABLE [dbo].[ReportRiskFactors] NOCHECK CONSTRAINT [FK_ReportRiskFactors_SEGusuaru_2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha que indica cuando se anula un factor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'CancellationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación de la anulación del factor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'CancellationObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del motivo de anulación del factor. Hace referencia a la tabla HCMOANULB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'CancellationReasonCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del profesional que anula el registro referencia  a la tabla INPROFSAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'CancellationProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la ultima vez que el profesional inactiva el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'LastInactivationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ultimo profesional que inactiva el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'LastInactivationProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la ultima vez que el profesional activa el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'LastActivationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ultimo profesional que activa el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'LastActivationProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1 - Activo 
2 - Inactivo 
3 - Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'DateCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que lo creó', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'UserCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación del riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intervención del riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'Intervention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla RiskFactor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'IdRiskFactor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación del paciente  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'Id';

