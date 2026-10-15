CREATE TABLE [dbo].[HCPARANTECEDENTESC] (
    [ID]            INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [UFUCODIGO]     CHAR (10) NOT NULL,
    [EXIGIRSIEMPRE] BIT       NOT NULL,
    CONSTRAINT [PK_HCPARANTC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARANTC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCPARANTECEDENTESC] NOCHECK CONSTRAINT [FK_HCPARANTC_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exigir siempre la parametrización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESC', @level2type = N'COLUMN', @level2name = N'EXIGIRSIEMPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESC', @level2type = N'COLUMN', @level2name = N'ID';

