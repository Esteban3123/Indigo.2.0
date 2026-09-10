CREATE TABLE [Payroll].[FondosContratos] (
    [FundId]                     INT          NOT NULL,
    [ContractId]                 INT          NOT NULL,
    [FundType]                   TINYINT      NOT NULL,
    [InitialDate]                DATE         NOT NULL,
    [EndingDate]                 DATE         NULL,
    [MembershipNumber]           VARCHAR (15) NOT NULL,
    [VoluntaryContribution]      BIT          NOT NULL,
    [VoluntaryContributionValue] NUMERIC (18) NULL,
    [State]                      BIT          NOT NULL,
    [CreationUser]               VARCHAR (20) NULL,
    [CreationDate]               DATETIME     NULL,
    [ModificationUser]           VARCHAR (20) NULL,
    [ModificationDate]           DATETIME     NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de nómina que registra la relación entre fondos (pensión, cesantías u otros) y contratos laborales, almacenando el tipo de fondo, el número de afiliación del empleado, las fechas de vigencia y el estado del vínculo. Permite registrar si el empleado realiza aportes voluntarios y, en caso afirmativo, el valor correspondiente. Incluye campos de auditoría para rastrear creación y modificación de cada registro.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'FondosContratos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'FondosContratos';
GO
