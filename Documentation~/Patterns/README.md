# UP-Common — Patterns (Phase 1)

This package provides a small set of production-friendly patterns for Unity projects.
Goal: low coupling, reusable architecture, and clean APIs for small-to-medium games.

## Contents
- Singleton
- Observer (EventBus)
- State Machine
- Command
- Pooling

---

## Singleton
**Purpose:** global access to a single instance.

**Scripts**
- `Singleton<T>`: for pure C# services.
- `MonoSingleton<T>`: for MonoBehaviour singletons (optional DontDestroyOnLoad).

**Use when**
- Global managers (Audio, EventBus, SaveSystem)
- Shared services (ConfigService, Localization)

---

## Observer (EventBus)
**Purpose:** decouple systems via typed events.

**Scripts**
- `IEvent`: marker for events (prefer `readonly struct`).
- `EventToken`: subscription handle (Dispose to unsubscribe).
- `EventBus`: publish/subscribe + main-thread queue.
- `EventBusExtensions`: MonoBehaviour shortcuts.
- `EventChannel<T>`: internal channel.

**Use when**
- UI updates from gameplay
- Gameplay systems communication without direct references
- Async/network callbacks -> main thread events

---

## State Machine
**Purpose:** structure behaviour by states instead of large if/switch.

**Scripts**
- `IState<TContext>` + `IStateInfo`
- `StateTransition<TContext>`
- `StateMachine<TContext>` (transitions, any-state, priority, timed transition, registry)

**Use when**
- Player controller / enemy AI
- UI flow (menu, popup)
- Game flow (Init -> Play -> End)

---

## Command
**Purpose:** encapsulate actions as objects (optional Undo/Redo).

**Scripts**
- `ICommand`
- `IUndoableCommand`
- `CommandInvoker` (Execute, Undo, Redo, MaxHistory)

**Use when**
- Undo/Redo actions (tools/editor-like gameplay)
- Input mapping to actions
- Replay / macro (basic)

---

## Pooling
**Purpose:** reuse GameObjects to avoid Instantiate/Destroy spikes.

**Scripts**
- `IPoolable` (OnSpawn/OnDespawn)
- `PoolItem` (internal metadata)
- `PoolManager` (WarmUp, Spawn, Spawn<T>, Despawn)

**Use when**
- Bullets/projectiles
- Enemies and VFX spawning frequently
- Reusable UI items

---

## Recommended Folder Layout
See `Runtime/Patterns/` for source code.
Docs: `Docs/Patterns/*.md` for each pattern.
