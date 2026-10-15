CREATE TABLE [Payroll].[ProfessionalRisk] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)    NOT NULL,
    [Name]             VARCHAR (20)    NOT NULL,
    [Percentage]       NUMERIC (18, 3) NOT NULL,
    [State]            BIT             NOT NULL,
    [CreationUser]     VARCHAR (20)    NOT NULL,
    [CreationDate]     DATETIME        NOT NULL,
    [ModificationUser] VARCHAR (20)    NULL,
    [ModificationDate] DATETIME        NULL,
    [TimeStamp]        ROWVERSION      NOT NULL,
    [ArlCode]          VARCHAR (7)     NULL,
    CONSTRAINT [PK_ProfessionalRisk] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código actividad económica de la ARL', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ProfessionalRisk', @level2type = N'COLUMN', @level2name = N'ArlCode';

