CREATE TABLE [dbo].[HCCTRNOTE] (
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGIST] DATETIME                                                                         NOT NULL,
    [TITNOTENF] CHAR (40)                                                                        NOT NULL,
    [NOTENFSUB] VARCHAR (MAX)                                                                    NULL,
    [NOTENFOBJ] VARCHAR (MAX)                                                                    NULL,
    [NOTENFANA] VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "NoteNursing_Ofuscado", 0)')   NULL,
    [CODNIVIMP] CHAR (2)                                                                         NULL,
    [FECREGSIS] DATETIME                                                                         CONSTRAINT [DF_HCCTRNOTE_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ID]        INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CONEXION]  VARCHAR (MAX)                                                                    NULL,
    [CODESPECI] CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_HCCTRNOTE_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCTRNOTE_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCTRNOTE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCTRNOTE_HCNIVIMPN] FOREIGN KEY ([CODNIVIMP]) REFERENCES [dbo].[HCNIVIMPN] ([CODNIVIMP]),
    CONSTRAINT [FK_HCCTRNOTE_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCCTRNOTE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCCTRNOTE_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCTRNOTE_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCCTRNOTE] NOCHECK CONSTRAINT [FK_HCCTRNOTE_ADCENATEN];


GO
ALTER TABLE [dbo].[HCCTRNOTE] NOCHECK CONSTRAINT [FK_HCCTRNOTE_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCTRNOTE] NOCHECK CONSTRAINT [FK_HCCTRNOTE_HCNIVIMPN];


GO
ALTER TABLE [dbo].[HCCTRNOTE] NOCHECK CONSTRAINT [FK_HCCTRNOTE_INESPECIA];


GO
ALTER TABLE [dbo].[HCCTRNOTE] NOCHECK CONSTRAINT [FK_HCCTRNOTE_INPACIENT];


GO
ALTER TABLE [dbo].[HCCTRNOTE] NOCHECK CONSTRAINT [FK_HCCTRNOTE_INPROFSAL];


GO
ALTER TABLE [dbo].[HCCTRNOTE] NOCHECK CONSTRAINT [FK_HCCTRNOTE_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCCTRNOTE_IPCODPACI_NUMINGRES_CODCENATE_CODNIVIMP_CODPROSAL_FECREGIST_TITNOTENF_UFUCODIGO]
    ON [dbo].[HCCTRNOTE]([IPCODPACI] ASC, [NUMINGRES] ASC)
    INCLUDE([CODCENATE], [CODNIVIMP], [CODPROSAL], [FECREGIST], [TITNOTENF], [UFUCODIGO]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCCTRNOTE_1]
    ON [dbo].[HCCTRNOTE]([IPCODPACI] ASC, [FECREGIST] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCCTRNOTE_IPCODPACI_CODCENATE_CODPROSAL_FECREGIST_NUMINGRES_TITNOTENF_UFUCODIGO]
    ON [dbo].[HCCTRNOTE]([IPCODPACI] ASC, [CODCENATE] ASC)
    INCLUDE([CODPROSAL], [FECREGIST], [NUMINGRES], [TITNOTENF], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCCTRNOTE_IPCODPACI_NUMINGRES_CODPROSAL_FECREGIST_FECREGSIS_NOTENFANA_NOTENFOBJ_NOTENFSUB_UFUCODIGO]
    ON [dbo].[HCCTRNOTE]([IPCODPACI] ASC, [NUMINGRES] ASC)
    INCLUDE([CODPROSAL], [FECREGIST], [FECREGSIS], [NOTENFANA], [NOTENFOBJ], [NOTENFSUB], [UFUCODIGO]);


GO
CREATE TRIGGER [dbo].[Tgr_ActualizaEspecialidadNULL_HCCTRNOTE] 
   ON  [dbo].[HCCTRNOTE] 
   AFTER INSERT
AS 
BEGIN
	
if exists(select 1 from inserted where CODESPECI is null )begin
		update hc set CODESPECI = prof.CODESPEC1
		from HCCTRNOTE hc
		inner join INPROFSAL prof on prof.CODPROSAL = hc.CODPROSAL
		where hc.ID in (select ID from inserted)
end


END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el código de la especialidad con la cual se firmó la historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que se agrega para almacenar información cuando se hacen notas de enfermería desde sesion de hemodialisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'CONEXION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla (Autonumérico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel de Importancia de la Nota', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nota Enfermeria Analisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'NOTENFANA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nota Enfermeria Objetivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'NOTENFOBJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nota Enfermeria Subjetivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'NOTENFSUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Titulo de la Nota', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'TITNOTENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'FECREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';

