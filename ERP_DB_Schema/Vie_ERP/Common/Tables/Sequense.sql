CREATE TABLE [Common].[Sequense] (
    [Id]      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Name]    VARCHAR (20) NOT NULL,
    [Pattern] VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_Sequense__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Sequense__Name]
    ON [Common].[Sequense]([Name] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Patrón que define la secuencia', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Sequense', @level2type = N'COLUMN', @level2name = N'Pattern';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la secuencia', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Sequense', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Sequense', @level2type = N'COLUMN', @level2name = N'Id';

