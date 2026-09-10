'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.Security
' Author           : WalterSierra
' Created          : 29-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-25
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports System
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
#End Region

''' <summary>
''' Esta clase utiliza un algoritmo de clave simétrica (Rijndael / AES) para cifrar y 
''' descifrar datos. Mientras rutinas de cifrado y descifrado, utilizan los mismos
''' parámetros para generar las claves.
''' La clase utiliza las funciones estáticas con código duplicado (valiables contantes)
''' </summary>
Public NotInheritable Class IndigoRijndael

    ''' <summary>
    ''' Cifra texto plano utilizando el algoritmo Rijndael de clave simétrica y devuelve un resultado codificado en base 64.
    ''' </summary>
    ''' <param name="text">la cadena de texto a encriptar.</param>
    ''' <returns>cadena de texto cifrada con formato como una cadena codificada en base 64</returns>
    Public Shared Function Encrypt(ByVal text As String) As String

        'Frase de contraseña, se utilizan para generar la clave de cifrado.
        Dim passPhrase As String = "Th3Pas5W0rd." 'cualquier string
        'valor que se utiliza con passPhrase para generar la contraseña 
        Dim saltValue As String = "S@1tValue" 'cualquier string
        'Algoritmo de hash usado para generar la contraseña. Los valores permitidos son: "MD5" y
        '"SHA1". Hash SHA1 son un poco más lento, pero más seguro que los hashes MD5
        'Dim hashAlgorithm As String = "SHA1" 'puede ser "MD5"
        'Número de iteraciones para generar contraseñas. Uno o dos iteraciones
        'debería ser suficiente.
        Dim passwordIterations As Integer = 5 'cualquier numero
        'Vector de inicialización (o IV). Este valor es necesario para cifrar el
        'primer bloque de datos en texto claro. Para la clase RijndaelManaged IV debe ser
        'exactamente 16 caracteres ASCII.
        Dim initVector As String = "@1B*c0D4#5F9$7H%" 'debe ser de 16 bytes
        'Tamaño de la clave de cifrado en bits. Los valores permitidos son: 128, 192 y 256.
        'Claves más largas son más seguras que las claves más cortas.
        Dim keySize As Integer = 256 ' puede ser 192 or 128

        'Convertir cadenas en matrices de bytes.
        Dim initVectorBytes As Byte()
        initVectorBytes = Encoding.ASCII.GetBytes(initVector)

        Dim saltValueBytes As Byte()
        saltValueBytes = Encoding.ASCII.GetBytes(saltValue)

        'Convertir nuestro texto plano en una matriz de bytes
        Dim plainTextBytes As Byte()
        plainTextBytes = Encoding.UTF8.GetBytes(text)

        'En primer lugar, debemos crear una contraseña, de la que será la clave derivada.
        'Esta contraseña será generada a partir de la frase de contraseña (passPhrase) especificado el saltValue
        'La contraseña se crea utilizando el algoritmo de hash especificado
        'La creación de contraseñas se puede hacer de varias iteraciones.
        'Dim password As PasswordDeriveBytes
        Dim password2 As Rfc2898DeriveBytes
        'Dim password As PasswordDeriveBytes

        password2 = New Rfc2898DeriveBytes(passPhrase, saltValueBytes, passwordIterations)
        'password = New PasswordDeriveBytes(passPhrase, _
        '                                   saltValueBytes, _
        '                                   hashAlgorithm, _
        '                                   passwordIterations)

        'Utilizamos la contraseña para generar bytes pseudo-aleatorios para el cifrado clave
        'Especificamos el tamaño de la clave en bytes (en lugar de bits).
        Dim keyBytes As Byte()
        'keyBytes = password.GetBytes(CInt(keySize / 8))
        keyBytes = password2.GetBytes(CInt(keySize / 8))

        ' Creamos el objeto sin inicializar (Rijndael).
        Dim symmetricKey As RijndaelManaged
        symmetricKey = New RijndaelManaged()

        ' Es razonable establecer el modo de cifrado de bloques de cifrado de encadenamiento (CBC).
        symmetricKey.Mode = CipherMode.CBC

        'Generamos el encriptador de los bytes de clave existente y la inicialización del vector. 
        'Tamaño de la clave se definirá en función del número de bytes de la clave.
        Dim encryptor As ICryptoTransform
        encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)

        ' Definimos la secuencia de memoria que se utilizará para almacenar los datos cifrados.
        Dim memoryStream As MemoryStream
        memoryStream = New MemoryStream()

        ' Definimos el flujo de cifrado (utilizar siempre el modo de escribir para el cifrado).
        Dim cryptoStream As CryptoStream
        cryptoStream = New CryptoStream(memoryStream, _
                                        encryptor, _
                                        CryptoStreamMode.Write)
        ' empezamos a encriptar.
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)

        ' terminamos el proceso de encriptamiento de la cadena.
        cryptoStream.FlushFinalBlock()

        ' Convertimos los datos cifrados de un flujo de memoria en una matriz de bytes
        Dim cipherTextBytes As Byte()
        cipherTextBytes = memoryStream.ToArray()

        ' cerramos ambos streams.
        memoryStream.Close()
        cryptoStream.Close()

        ' Convertimos los datos encriptados en una cadena codificada en base 64.
        Dim cipherText As String
        cipherText = Convert.ToBase64String(cipherTextBytes)

        ' retornamos la cadena encriptada
        Encrypt = cipherText
    End Function

    ''' <summary>
    ''' Descifra una cadena encriptada utilizando el algoritmo de cifrado Rijndael clave simétrica
    ''' </summary>
    ''' <param name="text">la cadena encriptada.</param>
    ''' <returns>Cadena de texto desencriptada</returns>
    Public Shared Function Decrypt(ByVal text As String) As String

        'Frase de contraseña, se utilizan para generar la clave de cifrado.
        Dim passPhrase As String = "Th3Pas5W0rd." 'cualquier string
        'valor que se utiliza con passPhrase para generar la contraseña 
        Dim saltValue As String = "S@1tValue" 'cualquier string
        'Algoritmo de hash usado para generar la contraseña. Los valores permitidos son: "MD5" y
        '"SHA1". Hash SHA1 son un poco más lento, pero más seguro que los hashes MD5
        'Dim hashAlgorithm As String = "SHA1" 'puede ser "MD5"
        'Número de iteraciones para generar contraseñas. Uno o dos iteraciones
        'debería ser suficiente.
        Dim passwordIterations As Integer = 5 'cualquier numero
        'Vector de inicialización (o IV). Este valor es necesario para cifrar el
        'primer bloque de datos en texto claro. Para la clase RijndaelManaged IV debe ser
        'exactamente 16 caracteres ASCII.
        Dim initVector As String = "@1B*c0D4#5F9$7H%" 'debe ser de 16 bytes
        'Tamaño de la clave de cifrado en bits. Los valores permitidos son: 128, 192 y 256.
        'Claves más largas son más seguras que las claves más cortas.
        Dim keySize As Integer = 256 ' puede ser 192 or 128

        ' Convierte cadenas de clave de cifrado en matrices de bytes 
        Dim initVectorBytes As Byte()
        initVectorBytes = Encoding.ASCII.GetBytes(initVector)

        Dim saltValueBytes As Byte()
        saltValueBytes = Encoding.ASCII.GetBytes(saltValue)

        ' Convertir la cadena encriptada en una matriz de bytes
        Dim cipherTextBytes As Byte()
        cipherTextBytes = Convert.FromBase64String(text)

        'En primer lugar, debemos crear una contraseña, de la cual la clave sera derivada. 
        'Esta contraseña será generada a partir de la especificada en  passPhrase y saltValueBytes. 
        'La contraseña se creará utilizando el algoritmo hash especificado. 
        'La creación de contraseñas se puede hacer en varias iteraciones.
        Dim password2 As Rfc2898DeriveBytes
        'Dim password As PasswordDeriveBytes

        password2 = New Rfc2898DeriveBytes(passPhrase, saltValueBytes, passwordIterations)
        'password = New PasswordDeriveBytes(passPhrase, _
        '                                   saltValueBytes, _
        '                                   hashAlgorithm, _
        '                                   passwordIterations)

        'Utilizamos la contraseña para generar bytes pseudo-aleatorios para la clave de cifrado
        'Especificamos el tamaño de la clave en bytes (en lugar de bits).
        Dim keyBytes As Byte()
        'keyBytes = password.GetBytes(CInt(keySize / 8))
        keyBytes = password2.GetBytes(CInt(keySize / 8))

        ' Creamos el objeto sin inicializar (Rijndael).
        Dim symmetricKey As RijndaelManaged
        symmetricKey = New RijndaelManaged()

        ' Es razonable establecer el modo de cifrado de bloques de cifrado de encadenamiento (CBC).
        symmetricKey.Mode = CipherMode.CBC

        'Generamos el descifrador de los bytes de clave existente y la inicialización del vector. 
        'el tamaño de la clave se definirá en función del número de los bytes clave
        Dim decryptor As ICryptoTransform
        decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)

        ' Definimos la secuencia de memoria que se utilizará para almacenar los datos cifrados.
        Dim memoryStream As MemoryStream
        memoryStream = New MemoryStream(cipherTextBytes)

        ' Definimos la secuencia de memoria que se utilizará para almacenar los datos cifrados. de tipo READ
        Dim cryptoStream As CryptoStream
        cryptoStream = New CryptoStream(memoryStream, _
                                        decryptor, _
                                        CryptoStreamMode.Read)

        'Dado que en este momento no sabemos cuál sera el tamaño de los datos descifrados
        'asignamos al búfer el tiempo suficiente para mantener el texto cifrado;
        'texto nunca es más largo que el texto cifrado.
        Dim plainTextBytes As Byte()
        ReDim plainTextBytes(cipherTextBytes.Length)

        ' empezamos a desencriptar.
        Dim decryptedByteCount As Integer
        decryptedByteCount = cryptoStream.Read(plainTextBytes, _
                                               0, _
                                               plainTextBytes.Length)

        ' cerramos ambos streams.
        memoryStream.Close()
        cryptoStream.Close()

        ' convertimos los datos desencriptados en un string
        Dim plainText As String
        plainText = Encoding.UTF8.GetString(plainTextBytes, _
                                            0, _
                                            decryptedByteCount)

        ' retornamos el string desencriptado
        Decrypt = plainText
    End Function
End Class