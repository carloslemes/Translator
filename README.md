#Merchant's Guide To The Galaxy
----------
##Marvin's Transaction Log

    Um utilitário de linha de comando para a contabilização de transações intergalaticas.

 > *In the beginning the Universe was created. This deeply angered many people and, in general, was seen as a bad idea.*

####Design da Solução

O Marvin foi elaborado como um utilitário de linha de comando para tornar o mais simples possível o ato de tomar notas.

Para realizar isso, utilizei de uma *factory* de comandos, que ao percorrer os comandos conhecidos, pergunta a eles se os argumentos de entrada servem. Uma vez que o *match* - isso é, a análise léxica do comando está correta, o segundo passo de *analysis* realiza a análise sintática do mesmo. Somente ao passar de todas as analises que o comando é executado.

Os comandos foram elaborados como:    

    - Assignment    
        Responsável por realizar a atribuição dos valores e salvar um arquivo .dat para armazenamento dos dados.
    
    - HowMany    
        Responsável por realizar a interpretação do comando que realiza a contabilização e conversão de valores
        
    - HowMuch
        Responsável por realizar a contagem dos valores já salvos
    
    - LoadFile
        Responsável por permitir a carga de notas para o arquivo dat.    
    
    - Help
        Responsável por exibir as funcionalidades suportadas pelo Marvin.

O Marvin cria dois arquivos de logs

    -MarvinTransaction.log
        Log das operações executadas

    -MarvinTransaction.dat
        Arquivo key/value para controle das associações realizadas pelo Marvin

Para facilitar a manipulação de algorismos romanos, foi criado o *type* *Roman*. O qual contém todas as especificidades dessa forma numérica, e consegue realizar a transição entre os símbolos textuais e a forma decima.

####Build
Execute o arquivo build.proj para a realizar da compilação do código.
> msbuild build.proj

O aplicativo será depositado no diretório *build*, denominado **Marvin.exe**

####Execução
- Marvin
-
    dont' panic! 
    Marvin is a trial application to help you log your intergalatic transactions.
    Marvin keeps yours transactions log at %HOMEPATH%\Documentos\Marvin
    Use -h --help to get help.

- Marvin -h --help
- 
    With Marvin's intergalactic transactions log you don't panic.
    Just enter your notes like this:
         glob is I
         prok is V
         pish is X
         tegj is L
         glob glob Silver is 34 Credits
         glob prok Gold is 57800 Credits
         pish pish Iron is 3910 Credits
    And when you need to know your status just ask me that:
         how much is pish tegj glob glob ?
         how many Credits is glob prok Silver ?
         how many Credits is glob prok Gold ?
         how many Credits is glob prok Iron ?
    Remeber intergalactic transactions follows similar convention to the roman numerals, they are based in seven symbols, as follows:
        Symbol Value
         I      1
         V      5
         X      10
         L      50
         C      100
         D      500
         M      1,000
    You can load your notes from one file. To do that is easy!
    Just enter -f or --file follow with the file directory
         Example Marvin -f C:\\user\\marvin\\Documents\\myNotes.txt 

- Marvin -f --file
- 
    Marvin input.txt
        pish tegj glob glob is 42
        glob prok Silver is 68 Credits
        glob prok Gold is 57800 Credits
        glob prok Iron is 782 Credits
        I have no idea what you are talking about

####Debug
Para realizar o debug da solução habilite a restauração de pacotes do Nuget, pois assim será possível compilar o projetos de testes. O Nuget está sendo utilizado para resolver a dependência com o NUnit.