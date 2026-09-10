CREATE TABLE [dbo].[ADRADICACIONQXD] (
    [ID]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDRADICACIONQX]  INT           NOT NULL,
    [IDLISTACHEQUEO]  INT           NOT NULL,
    [IDLISTACHEQUEOD] INT           NOT NULL,
    [CHEQUEADO]       BIT           NOT NULL,
    [CAMPOTEXTO]      VARCHAR (500) NULL,
    [CAMPOFECHA]      DATETIME      NULL,
    [CAMPOIDCITA]     INT           NULL,
    [URLADJUNTO]      VARCHAR (100) NULL,
    [TIPOCAMPO]       INT           NULL,
    [CodeHCCATDOCU]   CHAR (3)      NULL,
    [SupportDate]     DATETIME      NULL,
    CONSTRAINT [PK_ADRADICACIONQXD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADRADICACIONQXD_ADRADICACIONQXD] FOREIGN KEY ([IDRADICACIONQX]) REFERENCES [dbo].[ADRADICACIONQX] ([ID]),
    CONSTRAINT [FK_ADRADICACIONQXD_HCCATDOCU] FOREIGN KEY ([CodeHCCATDOCU]) REFERENCES [dbo].[HCCATDOCU] ([CODCATEGO]),
    CONSTRAINT [FK_ADRADICACIONQXD_HCLISTACC] FOREIGN KEY ([IDLISTACHEQUEO]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC]),
    CONSTRAINT [FK_ADRADICACIONQXD_HCLISTACD] FOREIGN KEY ([IDLISTACHEQUEOD]) REFERENCES [dbo].[HCLISTACD] ([CODCONSEC])
);




GO



GO



GO





GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [ADRADICACIONQXD_IDRADICACIONQX_CAMPOFECHA_TIPOCAMPO]
    ON [dbo].[ADRADICACIONQXD]([IDRADICACIONQX] ASC, [CAMPOFECHA] ASC, [TIPOCAMPO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de campo en la lista de chequeo: 1=Autorización, 2=Cita médica, 3=Cita externa, 4=Otro. INT, dominio controlado para categorizar el resultado del chequeo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'TIPOCAMPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'clasificación del campo:  1 - Autorización  2 - Cita  3 - Cita Externa  4 - Otro Campos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'TIPOCAMPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'TIPOCAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del archivo adjunto o resultado cargado en el campo. VARCHAR(100), ruta o enlace al documento, imagen o archivo soporte del chequeo realizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'URLADJUNTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado campo Adjunto, URL del adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'URLADJUNTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'URLADJUNTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID o identificador de la cita médica asociada al resultado del campo chequeable. INT, referencia a la cita programada o atendida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'CAMPOIDCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado campo ID cita medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'CAMPOIDCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'CAMPOIDCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha registrada como resultado del campo chequeable. DATETIME, captura la fecha de evento, consulta, procedimiento o documento evaluado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'CAMPOFECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado campo fecha ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'CAMPOFECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'CAMPOFECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación o descripción textual del resultado del campo chequeable. VARCHAR(500), notas, comentarios o hallazgos documentados durante el chequeo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'CAMPOTEXTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado campo observacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'CAMPOTEXTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'CAMPOTEXTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario de cumplimiento del chequeo: 1=Verificado/Completado, 0=No verificado. BIT, marca si el ítem de la lista fue revisado y validado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'CHEQUEADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Listado chequeo = 1 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'CHEQUEADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'CHEQUEADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del detalle o ítem individual dentro de la lista de chequeo. INT, PK de la tabla HCLISTACD, referencia al elemento específico a verificar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del Item deatlle de la lista de Chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la lista de chequeo principal o maestra. INT, FK a HCLISTACC, agrupa los ítems de verificación por protocolo o proceso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la radicación quirúrgica o cabecera de atención de la cual dependen los chequeos. INT, FK a ADRADICACIONQX, vincula los resultados al ingreso/procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'IDRADICACIONQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla de cabecera de radicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'IDRADICACIONQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'IDRADICACIONQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria autoincrementable de la tabla de detalles de chequeo. INT IDENTITY(1,1), identificador único de cada registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de soporte editable seleccionada por el usuario para validación o auditoría. DATETIME, permite correcciones posteriores de fechas en registros históricos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'SupportDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha soporte que el usuario selecciona y puede editar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'SupportDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'SupportDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de categoría de documento asociado al chequeo. CHAR(3), FK a HCCATDOCU, clasifica el tipo documental (autorización, cédula, examen, etc.) según catálogo estándar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'CodeHCCATDOCU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo de la categoria de documento, relacione con HCCATDOCU, para obtener.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'CodeHCCATDOCU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD', @level2type = N'COLUMN', @level2name = N'CodeHCCATDOCU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la lista de chequeo asociada a la radicación de cirugía (QX). Registra cada ítem verificado o pendiente en el proceso de admisión quirúrgica, incluyendo respuestas, fechas, archivos adjuntos y referencias a citas o documentos clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXD';
