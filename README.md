# Calculadora

  * Outra Aplicação: **Conversão** **e** **Resolução** **de** **Expressões** **Aritméticas**

Uma expressão aritmética é um conjunto de **operandos** e **operadores** ,  como os exemplos abaixo:

    1)A
    2)A + B
    3)A-B\*CD+E/F
    4)C – A / E +B

Os operandos, nos exemplos acima, são representados por letras. Os operadores são os sinais das operações.

Normalmente, os operadores são binários, ou seja, eles atuam sobre dois operandos. Dessamaneira, pode-se dizer que uma expressão é composta por duas sub-expressões mais simples, unidas por meio de um operador binário. Por sua vez, cada sub-expressão pode ser também composta por outras sub-expressões, unidas por outros operadores, e assim por diante, até que se chegue à expressão mais simples de todas, formada por apenas um operando, como a expressão 1 da lista acima.

A expressão 1 e 2 são as mais simples. Mas as duas seguintes podem conduzir a ambiguidades.Tanto se pode imaginar a sub-expressão A-B\*C/D+E/F como significando A menos o resultado de B vezes C dividido por D somado a E dividido por F, quanto A menos B, multiplicado por C dividido por D mais E, e tudo isso dividido por F.

Para evitar as ambiguidades, é atribuído a cada operador uma precedência, ou prioridade, sobre os demais. Assim,

| **Operador** | **Precedência** |
| --- | --- |
| ^ | 1 |
| \* , / | 2 |
| + , - | 3 |

ou seja, a operação de potenciação tem precedência sobre as demais, e as operações de soma e subtração têm precedência igual entre si, e em relação às outras três, são as menos prioritárias.

Assim, as expressões 1 a 4, dadas acima, significam o seguinte:

1. 1)O valor deA
2. 2)A somado aB
3. 3)C elevado a D, multiplicado porB, somado aE dividido por F. Tudo isso é subtraído deA
4. 4)A dividido por E, somado a B, tudo isso subtraído deC

Mas, e se quiséssemos alterar estes resultados, ou melhor, se fosse necessário mudar a ordem ditada pelas precedências? Então, seria preciso usar parênteses. Logo, os parênteses servem para alterar as precedências. Portanto, temos nova tabela de precedências:

| **Operador** | **Precedência** |
| --- | --- |
| ( | 1 |
| ^ | 2 |
| \* , / | 3 |
| + , - | 4 |

Portanto, poderíamos ter as expressões

1. 3)(A - (B \* (C D)) + (E / F)) (Idem à anterior) u

(((A - B) \*C)   D + E) / F



diferindo no resultado final, devido ao uso de parênteses em diferentes posições da expressão.

As expressões apresentadas são chamadas de expressões de **notação infixa** , pois os operadores estão entre os operandos.

Quando se começou a escrever os primeiros compiladores para linguagens de alto nível, a avaliação de expressões aritméticas trouxe uma série de problemas de solução difícil. Era muito difícil  solucionar uma expressão como a do 3º e 4º exemplos. Isto dificultou bastante a escrita de  comandos aritméticos de muitas linguagens, como COBOL, porexemplo.

Em COBOL, os comandos aritméticos, em geral, só permitem o uso de um operador. Logo, as expressões mais complexas, com vários operadores, nem sempre com a precedência normal, devem ser feitas por partes.

Assim, havia uma restrição forte às expressões aritméticas complexas, que tinham que ser escritas em várias partes separadas. Isto facilitava o trabalho do compilador, mas complicava a parte do programador. Note como o programador tinha de decompor a expressão em suas sub-expressões básicas, e calculá-las separadamente, antes de chegar ao resultado final.

O uso de expressões infixas com parênteses complica ainda mais a análise do compilador.

Em 1951, um matemático polonês chamado Jan Lukasiewicz, desenvolveu o conceito de expressões aritméticas que não necessitam de parênteses para retirar ambiguidades. Este conceito é o que que se passou a chamar de **notação Polonesa** ou **notação prefixa**. A partir dela desenvolveu-se outro tipo de notação, a **polonesa reversa** ou **pósfixa** , que é usada para os cálculos em calculadoras Hewlett Packard.

As expressões que usamos como exemplo seriam, em notação pósfixa, escritas da seguintemaneira:

| **Infixa** | **Pósfixa** |
|--|--|
|1)A|        A
|2)A+B |       AB+
|3)A - B \* C  D + E/F|        ABCD\*-EF/+
|4)C - A / E+B|        CAE/-B+ 
|5) A + (B \* C) / (K+G)   |     ABC\*KG+/+

A notação pósfixa permitiu a resolução dos problemas que ocorriam na avaliação de expressões aritméticas. Bastou criar um algorítmo que convertesse uma expressão infixa em pósfixa, e outro de



avaliação da expressão pósfixa, e isso facilitou sobremaneira o problema; assim, melhoraram-se vários compiladores, e mesmo a linguagem COBOL passou a dispor do comando COMPUTE, que permite o  uso de expressões infixas complexas e parentizadas.

**Compute** aux = A + (B \* C) / (K +G)

O compilador da linguagem analisa a expressão e, por meio de métodos semelhantes ao que estu- daremos a seguir, converte as diversas partes da expressão em seqüências pósfixas, que são posteriormente convertidas para código de máquina que faça os cálculos de duas em duas sub- expressões, maneira que se adapta à arquitetura dos computadores modernos.

Hoje existem linguagens que, por definição, são pósfixadas, como **FORTH** , tanto para as expressões aritméticas como para comandos. Isto retira do compilador o algorítmo de conversão infixa-pósfixa,tornando-o menor e mais rápido, embora aumente o tempo gasto pelo programador antes que ele se acostume a essa maneira de escrever, pois a lógica de programação normalmente continua  a mesma, mudando apenas a sintaxe dos comandos e expressões.



###### Conversão de Infixa para Pósfixa

Para desenvolvermos um algorítmo de conversão de notação infixa para pósfixa, observemos os passos a seguir:

1. verificar na sequência as sub-expressões que a formam
2. identificar a sub-expressão com maior precedência que ainda não tenha sido convertida; será algo dotipoA |B, onde AeB são operandos e|um operador binário.
3. Efetuar a conversão, colocando perador após os dois operandos : AB|. Esse resultadopassa a ser visto agora como um único operando.
4. Repetir as perações 2 e 3 até que se tenha terminado a conversão.


Os parênteses não são necessários, pois agora os operadores aparecem na ordem de execução do cálculo, não sendo mais preciso mudar essa ordem.

Quando dois operadores de mesma precedência aparecerem em sequência, deve-se converter pri- meiro a sub-expressão mais à esquerda, já que esta apareceu antes que a outra (portanto, tem precedência sobre a outra). No entanto, **quando**  **os**  **operadores**  **em**  **sequência forem** ^ **, tem**  **maior precedência**  **a**  **sub-expressão**  **à**  **direita**. Exemplos:



A + B+C        AB++C        AB+C+



**A** ^  **B** ^^ **C        A** ^^ **BC** ^^       **ABC** ^^^^

###### A / B^ C        ABC^/

A \* B\*C        AB\*C\*

(A + B) / C\*D        AB+C/D\*

(A + B) / (C \* D)        AB+CD\*/

**A + B** **C / (D**  **E \*** **F)**  **/ G**** / ****H        ABC**  **DE**  **F\*/G/H/+**

A - B / (C \* D E)        ABCDE\*/-



((A-B) / C - (D+E))   F G        ( AB- / C - DE+)  FG( AB-C/ -DE+)FG        AB-C/DE+-FG

**Algorítmo de conversão**

Converter        (A   B \* C - D + E / F / (G +H))

###### 4        3        2        1        1        2        24        1        00



Os números abaixo da expressão indicam a prioridade (precedência)  de cada operador.

Como se nota nos exemplos, a ordem de aparecimento dos operandos em ambas as sequências é a mesma. Logo, ao se fazer o algorítmo de conversão, deve-se escrever os operandos na sequênciapósfixa à medida que são lidos da sequência infixa.

E os operadores? Para eles, devemos posfixar primeiro os mais prioritários; devemos ir **guardando-**** os na pilha **até encontrar um outro com** prioridade maior no topo da pilha**. Quando isso acontecer, devemos colocar na sequência pósfixa os operadores guardados (desempilhando-os),**até o **** topo chegar **** a um **** operador de menor precedência** que aquele operador lido, ou a pilha ficar vazia.

A regra portanto, é **guardar**  **os**  **operadores de maior precedência na pilha** , até aparecer um de menor precedência que o topo, quando então deve-se descarregar a pilha até aparecer um operador de menor precedência que o lido, que então deve ser empilhado para uso futuro.

Assim, para a expressão acima, teremos :



| **Entrada Infixa** | **Pilha** | **Sequência Pósfixa** |
| --- | --- | --- |
| (AB\*C-D+E/F/(G+H)) | ( |   |
| AB\*C-D+E/E/(G+H)) | ( | A |
| B\*C-D+E/F/(G+H)) | ( | A |
| B\*C-D+E/F/(G+H)) | ( | AB |
| \*C-D+E/F/(G+H)) | (\* | AB |
| C-D+E/F/(G+H)) | (\* | ABC |
| -D+E/F/(G+H)) | (- | ABC\* |
| D+E/F/(G+H)) | (- | ABC\*D |
| +E/F/(G+H)) | (+ | ABC\*D- |
| E/F/(G+H)) | (+ | ABC\*D-E |
| /F/(G+H)) | (+/ | ABC\*D-E |
| F/(G+H)) | (+/ | ABC\*D-EF |
| /(G+H)) | (+/ | ABC\*D-EF/ |
| (G+H)) | (+/( | ABC\*D-EF/ |
| G+H)) | (+/( | ABC\*D-EF/G |
| +H)) | (+/(+ | ABC\*D-EF/G |
| H)) | (+/(+ | ABC\*D-EF/GH |
| )) | (+/(+ | ABC\*D-EF/GH+ |
| ) | (+/ | ABC\*D-EF/GH+/+ |

Para implementarmos o algorítmo, vamos precisar definir bem as prioridades dos operadores. Para isso, pode-se montar uma tabela de precedência, como a que se segue; ela será usada para de- terminar se o operador lido deve ou não ser desempilhado:



|   | ( |  | \* | / | + | - | ) | Símbolo Lido |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Símbolo na | ( | F | F | F | F | F | F | T | (último lido) |
| Pilha |  | F | T | T | T | T | T | T |   |
| (apareceu | \* | F | F | T | T | T | T | T |   |
| antes na | / | F | F | T | T | T | T | T |   |
| sequência) | + | F | F | F | F | T | T | T |   |
|   | - | F | F | F | F | T | T | T |   |
|   | ) | F | F | F | F | F | F | F |   |



Por exemplo, se **lemos** um símbolo&#39;-&#39;, eháno topo da pilha um &#39;(&#39;, este nãotem precedência sobre o &#39;-&#39;, e portanto &#39;-&#39; deve ser empilhado.

Se foi lido o símbolo &#39;(&#39;, e há qualquer outro no topo da pilha, o símbolo lido deverá ser empilhado, pois não tem precedência sobre nenhum outro.

No entanto, se lemos um &#39;\*&#39; e há um &#39;/&#39; no topo da pilha, embora na matemática tenham a mesma precedência, o que apareceu primeiro deve ser desempilhado e colocado na sequência pósfixa. Como &#39;/&#39; apareceu antes na sequência infixa ele tem prioridade sobre &#39;\*&#39; deverá ser desempilhado para aparecer na sequência pósfixa antes do &#39;\*&#39;, que, por sua vez, será empilhado antes de um elemento com precedênciamenor.

O procedimento relativo ao algoritmo, em Delphi, vem a seguir:



**Procedure** Converte\_de\_Infixa\_para\_Posfixa;

**Begin**

umaPilha:=TPilha.Create;        // Instancia e inicia aPilha

**While Not EOF** (Entrada) **Do Begin**

**Read** (Entrada,Simbolo\_Lido);

**If Not** (Simbolo\_Lido **In** [&#39;(&#39;,&#39;)&#39;,&#39;+&#39;,&#39;-&#39;,&#39;\*&#39;,&#39;/&#39;,&#39;&#39;]) **Then**  **Write** (Simbolo\_Lido)        // escreve Operando na tela

**Else        //**** operador **** Begin**

Parar := false;

**While** (not parar) and ( **not** umaPilha.estaVazia()) **and**

(Ha\_Precedencia(umaPilha.oTopo(), Simbolo\_Lido)) **Do**

**begin**

Operador\_com\_Maior\_Precedencia :=umaPilha.desempilhar();

**If** operador\_com\_Maior\_Precedencia &lt;&gt; &#39;(&#39; **then**  **Write** ( Operador\_com\_Maior\_Precedencia )

Else

Parar := true;

**End;**

**If** Simbolo\_Lido &lt;&gt; &#39;)&#39; **Then**

umaPilha.empilhar(Simbolo\_Lido)

**Else       ** { fará isso QUANDO o Pilha[TOPO] =&#39;(&#39; } Operador\_com\_Maior\_Precedencia        :=umaPilha.desempilhar();

**End** ;

**End** ;        // While not EOF

**While** not umaPilha.estaVazia() **Do** { Descarrega a Pilha Para a Saída }

**Begin**

Operador\_com\_Maior\_Precedencia :=umaPilha.desempilhar();

**If** Operador\_com\_Maior\_Precedencia &lt;&gt; &#39;(&#39; **Then**  **Write** (Operador\_com\_Maior\_Precedencia);

**End** ;

**End** ;

Ha\_Precedencia(A, B) é uma função que testa a precedência de A sobre B, usando os dados da tabela de precedência acima. A e B são operadores: A é o elemento do topo da pilha e B o últimosímbolo lido. Ela pode ser implementada com um comando switch/case, sem necessidade de umamatriz, já que os símbolos não estão em sequência na tabela ASCII e isso dificulta a indexação de matriz.

Ha\_Precedencia(&#39;\*&#39;,&#39;+&#39;) = **True** , pois se há um &#39;\*&#39; empilhado, ao se ler &#39;+&#39;, o &#39;\*&#39; deve aparecer antes na sequência pósfixa; logo, tem precedência, e portanto, será desempilhado. Exemplo:

###### Pilha        Seqüência Pósfixa

**A \* D +**** E        A**

\*        AD

+        AD\*

+        AD\*E

AD\*E+

**Cálculo do Valor de Expressão Pósfixa**

Para calcular o valor de uma expressão pósfixa, vamos supor que cada operando tem um valor.

A cada operando da expressão pósfixa (ou seja, um valor), faremos um empilhamento. Quando aparecer um operador, desempilharemos os dois últimos elementos colocados na pilha de operan- dos, e aplicaremos a eles a operação correspondente ao operador. O resultado desta operação será empilhado, e o processo continua até que se chegue ao final da sequência pósfixa. Nesse momento, haverá apenas um elemento na pilha, que será o resultado da expressão.

Como exemplo, suponha que temos a expressão ao lado:        AB-C/DE+-FG



Os valores dos operandos são os seguintes:

A=23        B=7        C=8        D=4        E=2        F=2        G =2

O processo de cálculo vem descrito abaixo:

| **Sequência pósfixa** AB-C/DE+-FGB-C/DE+-FG | **Pilha** vazia 23 |   |   |  {{ |  empilha empilha |  valor valor |  de de |
1. A}
2. B}
 |   |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| -C/DE+-FG | 23 | 7 |   | { | calcula | valor | da | subexpressão | } |
| C/DE+-FG | 16 |   |   | { | empilha | valor | de | C } |   |
| /DE+-FG | 16 | 8 |   | { | calcula | valor | da | subexpressão | } |
| DE+-FG | 2 |   |   | { | empilha | valor | de | D } |   |
| E+-FG | 2 | 4 |   | { | empilha | valor | de | E } |   |
| +-FG | 2 | 4 | 2 | { | calcula | valor | da | subexpressão | } |
| -FG | 2 | 6 |   | { | calcula | valor | da | subexpressão | } |
| FG | -4 | 2 |   | { | empilha | valor | de | F } |   |
| G | -4 | 2 | 2 | { | empilha | valor | de | G } |   |
|  | -4 | 2 | 2 | { | calcula | valor | da | subexpressão | } |
|  | -4 | 4 |   | { | calcula | valor | da | subexpressão | } |
|   | 256 |   |   |   |   |   |   |   |   |

A função que calcula e devolve o valor de uma expressão pósfixa vem a seguir:

**Function  ** Calcula\_Expressao\_Posfixa(Cadeia\_Posfixa:Cadeia):Integer {Real};

**Begin**

umaPilha := TPilha.Create();

**For** Atual:=1 **To Length** (Cadeia\_Posfixa) **Do Begin**

Simbolo:= Cadeia\_Posfixa[Atual];

**If Not** (Simbolo **In** [&#39;+&#39;,&#39;-&#39;,&#39;\*&#39;,&#39;/&#39;,&#39;&#39;]) **Then       ** //ÉOperandoumaPilha.empilhar(Valor\_de[Simbolo])

**Else Begin**

Operando2 := umaPilha.Desempilhar(); Operando1 := umaPilha.Desempilhar();

Valor := Calcula\_SubExpressao (Operando1, Simbolo, Operando2); umaPilha.empilhar(Valor);

**End** ; **End** ;

Resultado := umaPilha.desempilhar(); Calcula\_Expressao\_Posfixa:= Resultado;

**End** ;

Na implementação em Delphi do algorítmo, supôs-se que a sequência pósfixa estava colocada numa cadeia de caracteres. Pilha\_ de\_Valores é a pilha onde se armazenará os valores parciais das sub- expressões; o apontador dessa pilha é a variável Topo.

Valor\_de é um vetor, indexado por letras de &#39;A&#39; a &#39;Z&#39;, que contém o valor de cada um dos operandos da expressão. Assim sendo, Valor\_de[&#39;A&#39;] contém o valor do operando A, Valor\_de[&#39;B&#39;] contém o valor do operando B e assim sucessivamente.

Calcula\_SubExpressao é uma função que calcula o resultado da expressão aritmética simples definida pelos 2 operandos recém-desempilhados e pelo operador retirado da sequência pósfixa.

Como exercício, escreva a função Calcula\_SubExpressao em **C#**. Como projeto, você desenvolverá uma calculadora em C#, usando Windows forms como interface com o usuário.