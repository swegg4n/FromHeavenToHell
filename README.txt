För att styra båda karaktärerna behövs minst en handkontroll ikoplad. Man måste manuellt ställa in bool värdet PlayerDemonUsingMouse och PlayerAngelUsingMouse på GameManager-gameobjektet under playermanager skriptet.
För att ändra en ability drar du in dens scriptableobject (aoeClose01, New Dash Ability eller proj01) på rätt SerializedField i rätt prefab (PlayerAngel, PlayerDemon eller CorruptedBunnyDog).
Ability:nas värden ändras på scritableobjectet i inspectorn.
Spelarnas Health ändras GameManager-gameobjektet under playermanager skriptet.
Fiendernas Health ändras i deras prefab under enemybaseclass-scriptet.