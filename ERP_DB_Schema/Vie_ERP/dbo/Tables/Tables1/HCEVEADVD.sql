CREATE TABLE [dbo].[HCEVEADVD] (
    [ID]          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCEVENADVID] INT NOT NULL,
    [HCVALORTIPO] INT NOT NULL,
    CONSTRAINT [PK_HCEVEADVD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCEVEADVD_HCEVENADV] FOREIGN KEY ([HCEVENADVID]) REFERENCES [dbo].[HCEVENADV] ([ID])
);


GO
ALTER TABLE [dbo].[HCEVEADVD] NOCHECK CONSTRAINT [FK_HCEVEADVD_HCEVENADV];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Motivo adverso:  1-Vacunacion.  2-Transfución componontes sanguineos.  3-Medicamentos.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVEADVD', @level2type = N'COLUMN', @level2name = N'HCVALORTIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del valor del registro del motivo adverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVEADVD', @level2type = N'COLUMN', @level2name = N'HCEVENADVID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVEADVD', @level2type = N'COLUMN', @level2name = N'ID';

