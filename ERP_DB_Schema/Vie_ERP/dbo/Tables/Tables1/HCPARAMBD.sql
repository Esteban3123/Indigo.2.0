CREATE TABLE [dbo].[HCPARAMBD] (
    [ID]            INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCPARAMBID]    INT NOT NULL,
    [HCVALPARACLID] INT NOT NULL,
    CONSTRAINT [PK_HCPARAMBD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARAMBD_HCPARAMB] FOREIGN KEY ([HCPARAMBID]) REFERENCES [dbo].[HCPARAMB] ([ID]),
    CONSTRAINT [FK_HCPARAMBD_HCVALPARACL] FOREIGN KEY ([HCVALPARACLID]) REFERENCES [dbo].[HCVALPARACL] ([ID])
);


GO
ALTER TABLE [dbo].[HCPARAMBD] NOCHECK CONSTRAINT [FK_HCPARAMBD_HCPARAMB];


GO
ALTER TABLE [dbo].[HCPARAMBD] NOCHECK CONSTRAINT [FK_HCPARAMBD_HCVALPARACL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del valor predeterminado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMBD', @level2type = N'COLUMN', @level2name = N'HCVALPARACLID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del analito del paraclinico ambulatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMBD', @level2type = N'COLUMN', @level2name = N'HCPARAMBID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMBD', @level2type = N'COLUMN', @level2name = N'ID';

