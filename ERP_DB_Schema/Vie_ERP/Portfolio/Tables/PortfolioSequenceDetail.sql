CREATE TABLE [Portfolio].[PortfolioSequenceDetail] (
    [Id]                   INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequensePortfolioC] INT    NOT NULL,
    [IdSequense]           INT    NOT NULL,
    [IdOperatingUnit]      INT    NULL,
    [Next]                 BIGINT CONSTRAINT [DF_SequensePortfolioD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequensePortfolioD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequensePortfolioD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequensePortfolioD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequensePortfolioD_SequensePortfolioC] FOREIGN KEY ([IdSequensePortfolioC]) REFERENCES [Portfolio].[PortfolioSequence] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioSequenceDetail] NOCHECK CONSTRAINT [FK_SequensePortfolioD_OperatingUnit];


GO
ALTER TABLE [Portfolio].[PortfolioSequenceDetail] NOCHECK CONSTRAINT [FK_SequensePortfolioD_Sequense];


GO
ALTER TABLE [Portfolio].[PortfolioSequenceDetail] NOCHECK CONSTRAINT [FK_SequensePortfolioD_SequensePortfolioC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequensePortfolioC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';

