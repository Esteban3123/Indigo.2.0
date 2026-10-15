CREATE TABLE [dbo].[HCESCABRDES] (
    [ID]         INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCESCALAID] INT NOT NULL,
    [RESULTADOA] INT NULL,
    [RESULTADOB] INT NULL,
    [RESULTADOC] INT NULL,
    [RESULTADOD] INT NULL,
    CONSTRAINT [PK_HCESCADETALLE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCESCADETALLE_HCESCALAS] FOREIGN KEY ([HCESCALAID]) REFERENCES [dbo].[HCESCALAS] ([ID])
);


GO
ALTER TABLE [dbo].[HCESCABRDES] NOCHECK CONSTRAINT [FK_HCESCADETALLE_HCESCALAS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el resultado D', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'RESULTADOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el resulatdo C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'RESULTADOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el resulatdo B', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'RESULTADOB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el resultado A', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'RESULTADOA';

