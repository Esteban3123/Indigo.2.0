CREATE TABLE [Payroll].[RetirementReason] (
    [Id]                TINYINT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]              VARCHAR (20)  NOT NULL,
    [Name]              VARCHAR (100) NULL,
    [Compensation]      BIT           NOT NULL,
    [Severance]         BIT           CONSTRAINT [DF_RetirementReason_Serevance] DEFAULT ((0)) NOT NULL,
    [SeveranceInterest] BIT           CONSTRAINT [DF_RetirementReason_SeveranceInterest] DEFAULT ((0)) NOT NULL,
    [Prenotice]         BIT           CONSTRAINT [DF_RetirementReason_Prenotice] DEFAULT ((0)) NOT NULL,
    [Bonus]             BIT           CONSTRAINT [DF_RetirementReason_Bonus] DEFAULT ((0)) NOT NULL,
    [Vacations]         BIT           CONSTRAINT [DF_RetirementReason_Vacations] DEFAULT ((0)) NOT NULL,
    [State]             BIT           NOT NULL,
    [CreationUser]      VARCHAR (20)  NOT NULL,
    [CreationDate]      DATETIME      NOT NULL,
    [ModificationUser]  VARCHAR (20)  NULL,
    [ModificationDate]  DATETIME      NULL,
    [TimeStamp]         ROWVERSION    NOT NULL,
    CONSTRAINT [PK_RetirementReason__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_RetirementReason__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
CREATE NONCLUSTERED INDEX [PK_RetirementReason_CodeState]
    ON [Payroll].[RetirementReason]([Code] ASC, [State] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del motivo de retiro 1- Activo 0- Inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Vacations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prima/Aguinaldo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Bonus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Preaviso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Prenotice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intereses de cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'SeveranceInterest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Severance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Genera o no indemnizacion 1 - Genera, 0 - No Genera ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Compensation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del motivo de Retiro.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Motivo de Retiro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de los motivo de retiro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Id';

