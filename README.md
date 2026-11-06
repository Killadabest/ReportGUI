# ReportGUI for Gorilla Tag

---

A sleek, real-time synced, BepInEx-powered player reporting menu for Gorilla Tag. Easily view your current lobby, check system status, and report rule-breakers, toxic players, or cheaters directly in-game.

---

## Features

* **Player Reporting Menu:** Clean, anti-aliased high-res rounded UI designed to easily view room occupants and submit reports for toxicity, hate speech, or cheating with dedicated confirmation prompts.
* **Room Player List & Sorting:** Automatically detects and lists everyone in your current lobby, placing the Master Client at the top and supporting custom player name colors and Steam/PC platform badges.
* **Smart Text Stroke:** Automatically applies a clean thin white outline to pure black or dark text names so they remain perfectly readable against dark backgrounds.
* **Live API Status Sync:** Remote polling to monitor backend reporting system status, featuring fallback alerts and maintenance lockouts if the system goes offline.
* **Custom Audio Feedback:** Integrated sound effects streamed straight from GitHub for button interactions (`ui_click`) and report reason notifications (`ui_notifaction`).
* **Live FPS Counter:** Built-in performance meter in the top-left corner updating every 0.7 seconds with dynamic color-coding (Green for 60+, Orange for 30–59, Red for 0–29).
* **Draggable Interface:** Fully movable window layout so you can drag and position your reporting menu anywhere on screen.

---

## Installation

1. Make sure you have **BepInEx** installed for Gorilla Tag.
2. Download the latest `ReportGUI.dll` release file.
3. Place `ReportGUI.dll` into your `BepInEx/plugins` folder.
4. Launch Gorilla Tag and use the reporting menu in-game!

---

## Credits

* **Developer:** Killa
* **Plugin Template:** Astra
* **Comments:** Gemini
