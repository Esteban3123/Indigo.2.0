CREATE TABLE [dbo].[HCPARAMBFAV] (
    [ID]                  INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]           CHAR (10) NOT NULL,
    [UFUCODIGO]           CHAR (10) NULL,
    [CODESPECI]           CHAR (3)  NULL,
    [TIPOFAVORI]          INT       NOT NULL,
    [CODSERIPS]           CHAR (20) NOT NULL,
    [TIPOPARACLI]         INT       NULL,
    [USUARIOCREACION]     CHAR (20) NULL,
    [FECHACREACION]       DATETIME  NULL,
    [USUARIOMODIFICACION] CHAR (20) NULL,
    [FECHAMODIFICACION]   DATETIME  NULL,
    CONSTRAINT [PK_HCPARAMBFAV] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARAMBFAV_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCPARAMBFAV_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_HCPARAMBFAV_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[HCPARAMBFAV] NOCHECK CONSTRAINT [FK_HCPARAMBFAV_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCPARAMBFAV] NOCHECK CONSTRAINT [FK_HCPARAMBFAV_SEGusuaru_1];


GO
ALTER TABLE [dbo].[HCPARAMBFAV] NOCHECK CONSTRAINT [FK_HCPARAMBFAV_SEGusuaru_2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo paraclinico  1->Laboratorio  2->Imagenes Dx  3->Patologias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMBFAV', @level2type = N'COLUMN', @level2name = N'TIPOPARACLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Por Unidad Funcional  2 - Por Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMBFAV', @level2type = N'COLUMN', @level2name = N'TIPOFAVORI';

