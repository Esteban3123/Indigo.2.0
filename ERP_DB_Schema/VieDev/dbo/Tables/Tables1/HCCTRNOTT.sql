CREATE TABLE [dbo].[HCCTRNOTT] (
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGIST] DATETIME                                                                         NOT NULL,
    [TITNOTENF] CHAR (40)                                                                        NOT NULL,
    [NOTENFSUB] VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "NoteSubjetive_Ofuscado", 0)') NULL,
    [NOTENFOBJ] VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "NoteObjetive_Ofuscado", 0)')  NULL,
    [NOTENFANA] VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "NoteAnalysis_Ofuscado", 0)')  NULL,
    [CODNIVIMP] CHAR (2)                                                                         NULL,
    [FECREGSIS] DATETIME                                                                         CONSTRAINT [DF_HCCTRNOTT_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ID]        INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODESPECI] CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_HCCTRNOTT] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCTRNOTT_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCTRNOTT_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCTRNOTT_HCNIVIMPT] FOREIGN KEY ([CODNIVIMP]) REFERENCES [dbo].[HCNIVIMPT] ([CODNIVIMP]),
    CONSTRAINT [FK_HCCTRNOTT_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCCTRNOTT_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCCTRNOTT_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCTRNOTT_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCCTRNOTT] NOCHECK CONSTRAINT [FK_HCCTRNOTT_ADCENATEN];


GO
ALTER TABLE [dbo].[HCCTRNOTT] NOCHECK CONSTRAINT [FK_HCCTRNOTT_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCTRNOTT] NOCHECK CONSTRAINT [FK_HCCTRNOTT_HCNIVIMPT];


GO
ALTER TABLE [dbo].[HCCTRNOTT] NOCHECK CONSTRAINT [FK_HCCTRNOTT_INESPECIA];


GO
ALTER TABLE [dbo].[HCCTRNOTT] NOCHECK CONSTRAINT [FK_HCCTRNOTT_INPACIENT];


GO
ALTER TABLE [dbo].[HCCTRNOTT] NOCHECK CONSTRAINT [FK_HCCTRNOTT_INPROFSAL];


GO
ALTER TABLE [dbo].[HCCTRNOTT] NOCHECK CONSTRAINT [FK_HCCTRNOTT_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IDX_HCCTRNOTT_IPCODPACI]
    ON [dbo].[HCCTRNOTT]([IPCODPACI] ASC)
    INCLUDE([NUMINGRES], [CODCENATE], [UFUCODIGO], [CODPROSAL], [FECREGIST]);


GO
CREATE NONCLUSTERED INDEX [IX_HCCTRNOTT_IPCODPACI_CODCENATE]
    ON [dbo].[HCCTRNOTT]([IPCODPACI] ASC, [CODCENATE] ASC)
    INCLUDE([CODPROSAL], [FECREGIST], [NUMINGRES], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [IPCODPACI_FECREGIST]
    ON [dbo].[HCCTRNOTT]([IPCODPACI] ASC, [FECREGIST] ASC);


GO
CREATE TRIGGER [dbo].[Tgr_ActualizaEspecialidadNULL_HCCTRNOTT] 
   ON  [dbo].[HCCTRNOTT] 
   AFTER INSERT
AS 
BEGIN
	
if exists(select 1 from inserted where CODESPECI is null )begin
		update hc set CODESPECI = prof.CODESPEC1
		from HCCTRNOTT hc
		inner join INPROFSAL prof on prof.CODPROSAL = hc.CODPROSAL
		where hc.ID in (select ID from inserted)
end

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo de especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'CODESPECI';

