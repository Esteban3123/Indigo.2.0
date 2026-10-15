CREATE TABLE [dbo].[INESPECIA] (
    [CODESPECI]  CHAR (3)     NOT NULL,
    [DESESPECI]  CHAR (60)    NULL,
    [TIPATENCI]  BIT          NOT NULL,
    [INDAUDFOR]  NUMERIC (18) NOT NULL,
    [ESTADO]     BIT          NOT NULL,
    [TIPESPECI]  TINYINT      NULL,
    [MOSTRARWEB] BIT          NULL,
    [TIPCONWEB]  INT          NULL,
    CONSTRAINT [PK_INESPECIA] PRIMARY KEY CLUSTERED ([CODESPECI] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el tipo de consulta web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'TIPCONWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guarda el mostrar especialidad en citas web (si o no)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Especialidad  0- Ninguna  1- Oftalmología  2- Nutrición  3- Psicología  5- Mastología   6- Oncologia  7- Hematología  8- Cx oncológica  9- Ortopedia oncológica  10- Cx Plastica oncológica  11- Urológia oncológica  12-dermatología oncológica  13- Cuidados paliativos  14- Fisioterapia  15- Radioterapia  16-Cirugía  17-Especialidades Clínicas  18-Ginecología  19-Ginecología Oncológica  20-Oftamología Oncológica  21-Radiología  22-Trabajo social', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'TIPESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Diagnostico 1-Activo 2- Inactivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de atención  1=Oftalmología  2=Nutrición  3=Psicología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'TIPATENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'DESESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'CODESPECI';

