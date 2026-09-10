-- =============================================
-- Author:      (analisis asistido) Oscar Ivan Sierra Jaramillo
-- Create date: 2026-08-19
-- Modified:    2026-08-20 - Corrige duplicacion de facturas con multiples
--              consecutivos de conciliacion y Nota asignada al consecutivo
--              equivocado.
--
-- Description: Vista independiente para el reporte "Acta de Conciliacion"
--              (rptConciliationDocument). Trae las mismas columnas que hoy
--              consume el reporte via XPO y agrega la columna Nota
--              (Portfolio.PortfolioNote.Code).
--
--              IMPORTANTE (fix v2): antes se llegaba a ConciliationC via
--              Glosas.ConciliationD -> Glosas.GlosaPortfolioGlosada, que
--              acumula UNA FILA POR CADA conciliacion historica que haya
--              tocado la factura (nunca se borra ni se reemplaza). Eso
--              duplicaba la factura en cada consecutivo y mezclaba valores.
--
--              Ahora se llega por Glosas.GlosaMovementGlosaConciliation
--              (gmgc), la tabla de auditoria real que XPO/los SPs de
--              produccion (SP_SaveConciliation_Output,
--              SP_InvoiceTraceabilityConciliation) usan para saber a QUE
--              conciliacion especifica pertenece cada movimiento, con sus
--              valores propios de ESE evento (no el acumulado actual de
--              GlosaMovementGlosa). Tiene UNIQUE(GlosaMovementGlosaId,
--              ConciliationCId), por lo que cada fila de esta vista es
--              unica por movimiento+conciliacion.
--
--              La Nota sigue sin poder amarrarse a un consecutivo especifico
--              (Portfolio.PortfolioNote no tiene relacion con ConciliationC
--              en el modelo de datos actual) - se sigue cruzando por
--              InvoiceNumber contra Portfolio.AccountReceivable
--              (AccountReceivableType = 2), igual que en produccion
--              Glosas.SP_ReportListObjectionsReception. Si una factura
--              llegara a tener mas de una Nota real en distintos eventos de
--              conciliacion, ese caso residual no queda resuelto por esta
--              vista (limitacion de modelo de datos, no de este query).
--
-- Uso esperado: SELECT * FROM Glosas.ViewConciliationDocument
--               WHERE ConciliationId = @ConciliationId
--               (la vista NO filtra por conciliacion; el llamador decide)
-- =============================================
CREATE VIEW [Glosas].[ViewConciliationDocument]
AS
WITH cte_PortfolioNote AS
(
    -- Nota de cartera generada al aceptar la glosa/reiteracion/conciliacion.
    -- Solo notas confirmadas (Status = 2) con las observaciones de aceptacion IPS.
    -- Si hubiera mas de una nota por cuenta por cobrar, se toma la de menor Code/fecha
    -- (mismo criterio que usa hoy SP_ReportListObjectionsReception).
    SELECT
        pnara.AccountReceivableId,
        MIN(pn.Code)     AS NoteCode,
        MIN(pn.NoteDate) AS NoteDate
    FROM Portfolio.PortfolioNote pn WITH (NOLOCK)
    JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH (NOLOCK)
        ON pn.Id = pnara.PortfolioNoteId
    WHERE pn.Status = 2
      AND pn.Observations IN
      (
          'Aceptacion IPS glosa Subsanable - Glosa',
          'Aceptacion IPS glosa Subsanable - Reiteracion',
          'Aceptacion IPS glosa conciliacion'
      )
    GROUP BY pnara.AccountReceivableId
)
SELECT
    gmgc.Id                             AS Id,
    gmg.InvoiceNumber,
    gmg.CodeGlosa,
    cg.NameSpecific                     AS CodeGlosaName,
    gmg.ValueGlosado,
    gmg.ValueReiterated,
    gmgc.ValueAcceptedIPSconciliation,
    gmgc.ValueAcceptedEAPBconciliation,
    gmgc.RationaleConciliation,
    gord.DocumentType,
    cc.Id                               AS ConciliationId,
    cc.Nit,
    cc.NitName,
    cc.ConciliationDate,
    cc.ConciliationConsecutive,
    cc.Comment,
    ar.Id                               AS AccountReceivableId,
    pnote.NoteCode                      AS Nota,
    pnote.NoteDate                      AS NotaDate
FROM Glosas.GlosaMovementGlosaConciliation gmgc WITH (NOLOCK)
JOIN Glosas.GlosaMovementGlosa gmg WITH (NOLOCK)
    ON gmg.Id = gmgc.GlosaMovementGlosaId
JOIN Glosas.GlosaInvoiceDetail gid WITH (NOLOCK)
    ON gid.Id = gmg.InvoiceDetailId
JOIN Glosas.GlosaObjectionsReceptionD gord WITH (NOLOCK)
    ON gord.Id = gid.ObjectionsReceptionDId
JOIN Glosas.ConciliationC cc WITH (NOLOCK)
    ON cc.Id = gmgc.ConciliationCId
LEFT JOIN Common.ConceptGlosas cg WITH (NOLOCK)
    ON cg.Id = gmg.CodeGlosaId
LEFT JOIN Portfolio.AccountReceivable ar WITH (NOLOCK)
    ON ar.InvoiceNumber = gmg.InvoiceNumber
   AND ar.AccountReceivableType = 2
LEFT JOIN cte_PortfolioNote pnote
    ON pnote.AccountReceivableId = ar.Id
GO
