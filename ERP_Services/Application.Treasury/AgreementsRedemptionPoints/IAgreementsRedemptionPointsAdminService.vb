#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region
Public Interface IAgreementsRedemptionPointsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Función que obtiene todos los convenio de redención de puntos
    ''' </summary>
    ''' <returns>Lista de  convenio de redención de puntos</returns>
    ''' <remarks></remarks>
    Function ListAllAgreementsRedemptionPoints() As List(Of AgreementsRedemptionPoints)

    ''' <summary>
    ''' Función que obtiene por codigo el convenio de redención de puntos
    ''' </summary>
    ''' <param name="Code">Código del convenio de redención de puntos</param>
    ''' <returns>AgreementsRedemptionPoints</returns>
    ''' <remarks></remarks>
    Function GetAgreementsRedemptionPointsByCode(Code As String) As AgreementsRedemptionPoints

    ''' <summary>
    ''' Función para Almacenar un convenio de redención de puntos
    ''' </summary>
    ''' <param name="AgreementsRedemptionPoints">Objeto AgreementsRedemptionPoints</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveAgreementsRedemptionPoints(AgreementsRedemptionPoints As AgreementsRedemptionPoints, audit As AuditMessage, Optional idSequense As Long = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AgreementsRedemptionPoints)

    ''' <summary>
    ''' Función para Eliminar los convenio de redención de puntos
    ''' </summary>
    ''' <param name="AgreementsRedemptionPoints">Objeto AgreementsRedemptionPoints</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteAgreementsRedemptionPoints(AgreementsRedemptionPoints As AgreementsRedemptionPoints, audit As AuditMessage) As Domain.Base.Entities.ActionResult


    ''' <summary>
    ''' Cambiar el Estado del convenio de redención de puntos
    ''' </summary>
    ''' <param name="code">Code</param>
    ''' <param name="state">State</param>
    ''' <param name="audit">Audit</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function ChangeAgreementsRedemptionPointsStatus(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AgreementsRedemptionPoints)


    ''' <summary>
    ''' Función que obtiene todos los dettalles del convenio de redención de puntos
    ''' </summary>
    ''' <returns>Lista de  convenio de redención de puntos</returns>
    ''' <remarks></remarks>
    Function GetAgreementsRedemptionPointsDetails(Id As Integer) As List(Of AgreementsRedemptionPointsDetail)
End Interface
