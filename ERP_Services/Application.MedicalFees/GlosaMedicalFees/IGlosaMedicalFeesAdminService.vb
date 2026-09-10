#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

Public Interface IGlosaMedicalFeesAdminService
    Inherits IDisposable
    ''' <summary>
    ''' lista todos Honorarios medicos glosados
    ''' </summary>
    ''' <returns></returns>
    Function ListAllGlosaMedicalFees() As List(Of GlosaMedicalFees)

    ''' <summary>
    ''' Obtiene un Honorario medicos glosado por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function GetGlosaMedicalFeesByCode(Code As String) As GlosaMedicalFees

    ''' <summary>
    ''' Obtiene una Cuenta por Pagar por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function GetAccountPayableById(Id As Integer) As AccountPayable

    ''' <summary>
    '''Guarda o actualiza un registro
    ''' </summary>
    ''' <param name="GlosaMedicalFees">Objeto Responsible</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveGlosaMedicalFees(GlosaMedicalFees As GlosaMedicalFees, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of GlosaMedicalFees)

End Interface
