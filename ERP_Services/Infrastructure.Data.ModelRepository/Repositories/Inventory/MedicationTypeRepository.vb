Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class MedicationTypeRepository
    Inherits GenericRepository(Of MedicationType)
    Implements IMedicationTypeRepository

    Private _context As IGlobalModelUnitOfWork

#Region "Builder"
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Busca un registro de tipo de medicamento por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetMedicationTypeByCode(code As String) As MedicationType Implements IMedicationTypeRepository.GetMedicationTypeByCode
        If String.IsNullOrWhiteSpace(code) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As MedicationType In _context.MedicationType
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault()
        Return res
    End Function


#End Region
End Class
