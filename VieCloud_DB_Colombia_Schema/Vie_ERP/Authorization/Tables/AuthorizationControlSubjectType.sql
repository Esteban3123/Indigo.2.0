-- =============================================================================
-- NUEVO (2026-06-15) — Tarea 37517.
-- Catálogo del DISCRIMINADOR de tipo de sujeto de control polimórfico.
-- Da un hogar documentado y extensible al "tipo de sujeto" referenciado por
-- [Authorization].[AuthorizationControl].[SubjectType], evitando un enum mágico
-- disperso en código. Nuevos tipos de sujeto se agregan como datos (filas), sin
-- cambiar el esquema base (requisito de extensibilidad del modelo integral).
--
-- Valores canónicos previstos (sembrados por el equipo en despliegue/seed):
--   1 = SERVICIO     -> referencia lógica a [dbo].[INCUPSIPS].[CODSERIPS] (CUPS-RIPS)
--   2 = MEDICAMENTO  -> referencia lógica a [dbo].[IHLISTPRO].[CODPRODUC]
--   3 = INSUMO       -> referencia lógica a [dbo].[IHLISTPRO].[CODPRODUC]
--   4 = URGENCIA     -> referencia lógica a [dbo].[ADATEINIU].[CODCONCEC] (clave del AIU)
--   5 = ESTANCIA     -> PUNTO DE EXTENSIÓN DIFERIDO. Ver TODO abajo.
--
-- TODO (ESTANCIA — diferido, mejora incremental posterior):
--   El valor 'ESTANCIA' (5) existe en este catálogo como punto de extensión
--   documentado, PERO la tabla fuente del control de estancias queda PENDIENTE de
--   identificar por el usuario y NO debe inferirse (ver MODELO-SUSCEPTIBILIDAD-Y-CONTROL.md
--   §5 / §8 P1). NO se crea FK ni se infiere el nombre de la tabla destino de estancias.
--   Cuando el usuario confirme la tabla fuente, se documentará su referencia lógica aquí.
--
-- Referencias a maestros: LÓGICAS (no se declaran FK físicas). El sujeto es
-- polimórfico (una sola columna de código apunta a maestros distintos según el
-- tipo), por lo que un FK físico es inviable; además, el aislamiento del dominio
-- de Authorización (ADR-007) desaconseja acoplar el modelo de control al esquema
-- clínico transaccional. La susceptibilidad NO se modela aquí: se resuelve contra
-- [dbo].[ADCONFSER]/[dbo].[ADCONFSERD] (ADR-007).
-- =============================================================================
CREATE TABLE [Authorization].[AuthorizationControlSubjectType] (
    [SubjectType]      TINYINT       NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [MasterTableHint]  VARCHAR (128) NULL,
    [Status]           BIT           NOT NULL CONSTRAINT [DF_AuthorizationControlSubjectType_Status] DEFAULT ((1)),
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL CONSTRAINT [DF_AuthorizationControlSubjectType_CreationDate] DEFAULT (GETDATE()),
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    CONSTRAINT [PK_AuthorizationControlSubjectType] PRIMARY KEY CLUSTERED ([SubjectType] ASC),
    CONSTRAINT [UQ_AuthorizationControlSubjectType_Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo del discriminador de tipo de sujeto de control polimórfico del módulo de Autorización. Cada fila define un tipo de sujeto que un registro de [Authorization].[AuthorizationControl] puede referenciar (SERVICIO, MEDICAMENTO, INSUMO, URGENCIA y, como punto de extensión diferido, ESTANCIA). Permite agregar nuevos tipos de sujeto como datos, sin alterar el esquema base. La susceptibilidad NO se modela aquí; se resuelve contra [dbo].[ADCONFSER]/[ADCONFSERD].', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (TINYINT) discriminador del tipo de sujeto de control. Valores canónicos: 1=SERVICIO, 2=MEDICAMENTO, 3=INSUMO, 4=URGENCIA, 5=ESTANCIA (punto de extensión diferido). Es la clave referenciada por [Authorization].[AuthorizationControl].[SubjectType].', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'SubjectType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'SubjectType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código textual estable (VARCHAR 20) del tipo de sujeto para uso en código y APIs (p.ej. ''SERVICIO'', ''MEDICAMENTO'', ''INSUMO'', ''URGENCIA'', ''ESTANCIA''). Único en el catálogo.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo (VARCHAR 100) del tipo de sujeto de control para presentación en UI y reportes.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pista documental (VARCHAR 128, NULL) del maestro lógico al que apunta el código del sujeto de este tipo (p.ej. ''dbo.INCUPSIPS.CODSERIPS'', ''dbo.IHLISTPRO.CODPRODUC'', ''dbo.ADATEINIU.CODCONCEC''). Es solo documentación; NO crea FK. Para ESTANCIA queda NULL hasta que el usuario confirme la tabla fuente (pendiente, no inferir).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'MasterTableHint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'MasterTableHint';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del tipo de sujeto (BIT): 1=activo (seleccionable), 0=inactivo. Permite retirar un tipo del uso sin borrar historial. Por defecto 1.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario (VARCHAR 20) que creó la fila del catálogo. Auditoría de origen.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación de la fila del catálogo. Por defecto GETDATE().', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario (VARCHAR 20, NULL) que realizó la última modificación de la fila del catálogo.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME, NULL) de la última modificación de la fila del catálogo.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'ModificationDate';
