# ufabc-jogos

Projeto para disciplina de Introdução à Programação de Jogos

| Aluno   |      Ra
|---------|:-------------:
|Arthur Henrique Fernandes|11061816
|Emerson Almeida Matos|11101015
|Vitor Guilherme Antunes|11071815

## Escopo

Nosssa proposta é desenvolver um jogo de FPS Multiplayer ambientado no campus da UFABC, e dessa forma o jogo terá as seguintes dinamicas:

1. O jogo é composto por dois times, terroristas e contra-terroristas, que se confrontam em rodadas diferentes.
2. Um time deve vencer um número fixo de rodadas para ganhar uma partida.
3. Para cada rodada, o objetivo dos terroristas é plantar uma bomba em um local pré determinado e os contra-terroristas devem impedir.
    1. Os terroristas vencem a rodada caso
        1. Matarem todos os contra-terroristas
        2. Plantarem e protegerem a bomba até ela explodir
    2. Os contra-terroristas vencem a rodada caso
        1. Matarem todos os terroristas antes de terem plantado a bomba
        2. A bomba plantada for desarmada com sucesso
        3. O tempo acabe sem que os terroristas plantem a bomba
4. Quando uma rodada começa os terroristas tem um tempo limite para plantarem a bomba, caso a bomba seja plantada os contra-terroristas terão um tempo limite para desarma-la.
5. Na primeira rodada, todos os jogadores começam com os mesmos itens e com a mesma quantidade de dinheiro.
6. Nas rodadas posteriores, cada jogador receberá uma quantia em dinheiro com base no seu desempenho da rodada anterior
7. A cada rodada ambas as equipes podem comprar equipamentos extra com o dinheiro recebido
