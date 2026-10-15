CREATE TABLE [dbo].[HCDIAGPCE] (
    [CODDIAGPCE] INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODDIAGENF] VARCHAR (5) NOT NULL,
    CONSTRAINT [FK_HCDIAG_PCE] PRIMARY KEY CLUSTERED ([CODDIAGPCE] ASC),
    CONSTRAINT [FK_HCDIAGPCE_HCDIAGENF] FOREIGN KEY ([CODDIAGENF]) REFERENCES [dbo].[HCDIAGENF] ([CODDIAGENF])
);


GO
ALTER TABLE [dbo].[HCDIAGPCE] NOCHECK CONSTRAINT [FK_HCDIAGPCE_HCDIAGENF];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del diagnostico e enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del diagnostico del plan cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGPCE';

